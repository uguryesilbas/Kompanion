using Microsoft.Extensions.Caching.Distributed;
using Kompanion.Application.Caching;
using Kompanion.Application.Serializations.Json;

namespace Kompanion.Infrastructure.Caching.Services;

internal sealed class MemoryCacheService(IDistributedCache distributedCache) : ICacheService
{
    public async Task<bool> KeyExistsAsync(string cacheKey, int database = 0)
    {
        string result = await distributedCache.GetStringAsync(cacheKey);

        return !string.IsNullOrWhiteSpace(result);
    }

    public async Task<string> GetAsync(string cacheKey, int database = 0)
    {
        if (string.IsNullOrWhiteSpace(cacheKey))
        {
            return string.Empty;
        }

        return await distributedCache.GetStringAsync(cacheKey);
    }

    public async Task<T> GetAsync<T>(string cacheKey, int database = 0) where T : class
    {
        string result = await GetAsync(cacheKey);

        return !string.IsNullOrWhiteSpace(result) ? JsonSerialization.Deserialize<T>(result) : null;
    }

    public async Task<bool> SetAsync(string cacheKey, string value, TimeSpan? expiry = null, int database = 0)
    {
        if (string.IsNullOrWhiteSpace(cacheKey))
        {
            return false;
        }

        await distributedCache.SetStringAsync(cacheKey, value, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = expiry.HasValue ? DateTimeOffset.Now.Add(expiry.Value) : null
        });

        return true;
    }

    public async Task<bool> SetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null, int database = 0)
    {
        return await SetAsync(cacheKey, JsonSerialization.Serialize(value), expiry);
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> action, TimeSpan? expiry = null, int database = 0) where T : class
    {
        T result = await GetAsync<T>(cacheKey);

        if (result is not null)
        {
            return result;
        }

        T actionResult = await action();

        if (actionResult is null)
        {
            return null;
        }

        return await SetAsync(cacheKey, actionResult, expiry) ? actionResult : null;
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<T> action, TimeSpan? expiry = null, int database = 0) where T : class
    {
        T result = await GetAsync<T>(cacheKey);

        if (result is not null)
        {
            return result;
        }

        T actionResult = action();

        if (actionResult is null)
        {
            return null;
        }

        return await SetAsync(cacheKey, actionResult, expiry) ? actionResult : null;
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null, int database = 0) where T : class
    {
        T result = await GetAsync<T>(cacheKey);

        if (result is not null)
        {
            return result;
        }

        return await SetAsync(cacheKey, value, expiry) ? value : null;
    }

    public async Task<List<T>> GetOrSetAsync<T>(string cacheKey, List<T> values, TimeSpan? expiry = null, int database = 0) where T : class
    {
        List<T> result = await GetAsync<List<T>>(cacheKey);

        if (result is { Count: > 0 })
        {
            return result;
        }

        return await SetAsync(cacheKey, JsonSerialization.Serialize(values), expiry) ? values : null;
    }

    public async Task<bool> ClearAsync(string cacheKey, int database = 0)
    {
        await distributedCache.RemoveAsync(cacheKey);

        return true;
    }

    [Obsolete("Get all keys function could not be found in memory cache", true)]
    public Task ClearAllAsync(int database = 0)
    {
        throw new NotImplementedException();
    }
}