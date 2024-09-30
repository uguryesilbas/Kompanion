using StackExchange.Redis;
using System.Text.Json;
using Kompanion.Application.Caching;
using Kompanion.Application.Serializations.Json;

namespace Kompanion.Infrastructure.Caching.Services;

internal sealed class RedisCacheService(IConnectionMultiplexer connectionMultiplexer) : ICacheService
{
    private IDatabase GetDatabase(int database) => connectionMultiplexer.GetDatabase(database);

    public async Task<bool> KeyExistsAsync(string cacheKey, int database = 0)
    {
        return await GetDatabase(database).KeyExistsAsync(cacheKey);
    }

    public async Task<string> GetAsync(string cacheKey, int database = 0)
    {
        if (string.IsNullOrWhiteSpace(cacheKey))
        {
            return string.Empty;
        }

        return await GetDatabase(database).StringGetAsync(cacheKey);
    }

    public async Task<T> GetAsync<T>(string cacheKey, int database = 0) where T : class
    {
        if (string.IsNullOrWhiteSpace(cacheKey))
        {
            return null;
        }

        RedisValue redisValue = await GetDatabase(database).StringGetAsync(cacheKey);

        return !redisValue.IsNullOrEmpty ? JsonSerialization.Deserialize<T>(redisValue.ToString()) : null;
    }

    public async Task<bool> SetAsync(string cacheKey, string value, TimeSpan? expiry = null, int database = 0)
    {
        if (string.IsNullOrWhiteSpace(cacheKey) || string.IsNullOrWhiteSpace(value) || (expiry.HasValue && expiry.Value <= TimeSpan.Zero))
        {
            return false;
        }

        return await GetDatabase(database).StringSetAsync(cacheKey, value, expiry, When.Always);
    }

    public async Task<bool> SetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null, int database = 0)
    {
        if (string.IsNullOrWhiteSpace(cacheKey) || value is null)
        {
            return false;
        }

        return await SetAsync(cacheKey, JsonSerialization.Serialize(value), expiry, database);
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> action, TimeSpan? expiry = null, int database = 0) where T : class
    {
        T result = await GetAsync<T>(cacheKey);

        if (result is not null)
        {
            return result;
        }

        T actionResult = await action();

        return await SetAsync(cacheKey, JsonSerializer.Serialize(actionResult), expiry, database)
            ? actionResult
            : null;
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<T> action, TimeSpan? expiry = null, int database = 0) where T : class
    {
        T result = await GetAsync<T>(cacheKey);

        if (result is not null)
        {
            return result;
        }

        T actionResult = action();

        return await SetAsync(cacheKey, JsonSerializer.Serialize(actionResult), expiry, database)
            ? actionResult
            : null;
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null, int database = 0) where T : class
    {
        T result = await GetAsync<T>(cacheKey);

        if (result is null)
        {
            return await SetAsync(cacheKey, JsonSerializer.Serialize(value), expiry, database)
                ? value
                : null;
        }

        return result;
    }

    public async Task<List<T>> GetOrSetAsync<T>(string cacheKey, List<T> values, TimeSpan? expiry = null, int database = 0) where T : class
    {
        List<T> result = await GetAsync<List<T>>(cacheKey);

        if (result is not { Count: > 0 })
        {
            return await SetAsync(cacheKey, JsonSerializer.Serialize(values), expiry, database)
                ? values
                : null;
        }

        return result;
    }

    public async Task<bool> ClearAsync(string cacheKey, int database = 0)
    {
        return await GetDatabase(database).KeyDeleteAsync(cacheKey);
    }

    public async Task ClearAllAsync(int database = 0)
    {
        IServer[] servers = GetDatabase(database).Multiplexer.GetServers();

        foreach (IServer server in servers)
        {
            await server.FlushAllDatabasesAsync();
        }
    }
}