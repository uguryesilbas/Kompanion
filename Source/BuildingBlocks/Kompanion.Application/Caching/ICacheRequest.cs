namespace Kompanion.Application.Caching;

public interface ICacheRequest
{
    string CacheKey { get; }
    int? Database { get; }
    TimeSpan? CacheExpiry { get; }
}