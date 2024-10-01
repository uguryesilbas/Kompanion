using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Kompanion.Application.Caching;
using Kompanion.Application.Extensions;
using Kompanion.Infrastructure.Caching.Options;
using Kompanion.Infrastructure.Caching.Services;

namespace Kompanion.Infrastructure.Caching;

public static class DependencyInstaller
{
    public static IServiceCollection AddDistributedCache(this IServiceCollection services, string redisSectionName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(redisSectionName, nameof(redisSectionName));

        RedisOptions redisOptions = services.GetOptions<RedisOptions>(redisSectionName);

        services.AddDistributedCache(redisOptions);

        return services;
    }

    public static IServiceCollection AddDistributedCache(this IServiceCollection services, Action<RedisOptions> action)
    {
        RedisOptions redisOptions = new();

        action.Invoke(redisOptions);

        return services.AddDistributedCache(redisOptions);
    }


    private static IServiceCollection AddDistributedCache(this IServiceCollection services, RedisOptions options)
    {
        if (options is null || !options.Enabled)
        {
            services.AddDistributedMemoryCache();

            services.AddSingleton<ICacheService, MemoryCacheService>();

            return services;
        }

        ConfigurationOptions redisConfigurationOptions = new()
        {
            AbortOnConnectFail = options.AbortOnConnectFail,
            AsyncTimeout = options.AsyncTimeout,
            ConnectTimeout = options.ConnectTimeout,
            SyncTimeout = options.SyncTimeout,
            ConnectRetry = options.ConnectRetry,
            Password = options.Password,
            DefaultDatabase = options.DefaultDatabase
        };

        foreach (RedisEndpointOptions endpointOptions in options.Endpoints)
        {
            redisConfigurationOptions.EndPoints.Add(endpointOptions.Host, endpointOptions.Port);
        }

        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfigurationOptions);

        services
            .AddSingleton<IConnectionMultiplexer>(connectionMultiplexer)
            .AddSingleton<ICacheService, RedisCacheService>();


        return services;
    }
}