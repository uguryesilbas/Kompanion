namespace Kompanion.Application.Caching;

public interface ICacheService
{
    Task<bool> KeyExistsAsync(string cacheKey, int database = 0);

    Task<string> GetAsync(string cacheKey, int database = 0);

    Task<T> GetAsync<T>(string cacheKey, int database = 0) where T : class;

    Task<bool> SetAsync(string cacheKey, string value, TimeSpan? expiry = null, int database = 0);

    Task<bool> SetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null, int database = 0);

    Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> action, TimeSpan? expiry = null, int database = 0) where T : class;

    Task<T> GetOrSetAsync<T>(string cacheKey, Func<T> action, TimeSpan? expiry = null, int database = 0) where T : class;

    Task<T> GetOrSetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null, int database = 0) where T : class;

    Task<List<T>> GetOrSetAsync<T>(string cacheKey, List<T> values, TimeSpan? expiry = null, int database = 0) where T : class;

    Task<bool> ClearAsync(string cacheKey, int database = 0);

    Task ClearAllAsync(int database = 0);
}