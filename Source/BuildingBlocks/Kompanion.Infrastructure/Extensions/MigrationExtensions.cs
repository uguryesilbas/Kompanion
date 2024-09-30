using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kompanion.Infrastructure.Database.Abstracts;

namespace Kompanion.Infrastructure.Extensions;

public static class MigrationExtensions
{
    public static IServiceCollection Migration<TContext>(this IServiceCollection services) where TContext : BaseDbContext
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        serviceProvider.Migration<TContext>();

        return services;
    }

    public static IServiceProvider Migration<TContext>(this IServiceProvider services) where TContext : BaseDbContext
    {
        services.GetRequiredService<TContext>().Database.Migrate();

        return services;
    }

    public static WebApplication Migration<TContext>(this WebApplication app) where TContext : BaseDbContext
    {
        app.Services.GetRequiredService<TContext>().Database.Migrate();

        return app;
    }
}