using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kompanion.Infrastructure.Database.Abstracts;
using Kompanion.Infrastructure.Database.Interceptors;

namespace Kompanion.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection UseSqlServerDatabase<TContextInterface, TContext>(this IServiceCollection services, string connectionStringSectionName) where TContext : BaseDbContext
    {
        return services.AddDbContext<TContextInterface, TContext>(connectionStringSectionName);
    }

    public static IServiceCollection UsePostgreSqlDatabase<TContextInterface, TContext>(this IServiceCollection services, string connectionStringSectionName) where TContext : BaseDbContext
    {
        return services.AddDbContext<TContextInterface, TContext>(connectionStringSectionName, false);
    }

    private static IServiceCollection AddDbContext<TContextInterface, TContext>(this IServiceCollection services, string connectionStringSectionName, bool useSqlServerDatabase = true) where TContext : BaseDbContext
    {
        if (string.IsNullOrWhiteSpace(connectionStringSectionName))
        {
            throw new ArgumentNullException(nameof(connectionStringSectionName), "Connection string section name can not be null or empty.");
        }

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        IWebHostEnvironment environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        string connectionString = configuration.GetConnectionString(connectionStringSectionName) ??
                                  throw new ArgumentNullException($"Connection string section name: {connectionStringSectionName}", "Connection string could not be found");

        services.AddScoped<ISaveChangesInterceptor, TrackableEntityInterceptor>();

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<TContext>((sp, option) =>
        {
            option.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            _ = useSqlServerDatabase
                ? option.UseSqlServer(connectionString)
                : option.UseNpgsql(connectionString);

            if (environment.IsDevelopment())
            {
                option.EnableDetailedErrors();
                option.EnableSensitiveDataLogging();
            }
        });

        services.AddScoped(typeof(TContextInterface), typeof(TContext));

        return services;
    }
}