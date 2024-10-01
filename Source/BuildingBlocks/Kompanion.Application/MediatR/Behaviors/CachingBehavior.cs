using MediatR;
using Microsoft.Extensions.Logging;
using Kompanion.Application.Caching;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.MediatR.Behaviors;

internal class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest where TResponse : ApiResponse
{
    private const double DefaultCacheExpirationInHours = 1;

    private readonly ICacheService _cacheService;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(ICacheService cacheService, ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICacheRequest cacheRequest)
        {
            return await next();
        }

        string cacheKey = cacheRequest.CacheKey;

        TResponse cacheResponse = await _cacheService.GetAsync<TResponse>(cacheKey);

        if (cacheResponse is not null)
        {
            _logger.LogTrace("Response retrieved {TRequest} from cache. CacheKey: {CacheKey}", typeof(TRequest).FullName, cacheKey);

            return cacheResponse;
        }

        TResponse response = await next();

        if (response.IsSuccessStatusCode)
        {
            TimeSpan cacheExpiry = cacheRequest.CacheExpiry ?? TimeSpan.FromHours(DefaultCacheExpirationInHours);

            await _cacheService.SetAsync(cacheKey, response, cacheExpiry, cacheRequest.Database ?? 0);
        }

        return response;
    }
}