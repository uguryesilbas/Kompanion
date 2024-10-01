using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Kompanion.ECommerce.Infrastructure.Context;
using Kompanion.ECommerce.Domain.Bank;
using Kompanion.ECommerce.Infrastructure.Domain;
using Kompanion.ECommerce.Domain.Product;
using Kompanion.Infrastructure.Caching;
using Kompanion.Application;

namespace Kompanion.ECommerce.Infrastructure;

public static class Bootstrapper
{
    public static WebApplicationBuilder AddECommerceInfrastructures(this WebApplicationBuilder builder)
    {
        //builder.AddSerilog(ApplicationConstants.ConfigurationSectionConstants.LogSection);

        builder.Services.AddDistributedCache(ApplicationConstants.ConfigurationSectionConstants.RedisSection);

        builder.Services.AddDbContext();

        builder.Services.AddRepositories();

        return builder;
    }

    private static void AddDbContext(this IServiceCollection services)
    {
        const string ConnectionStringSectionName = "ECommerceApi";

        services.AddScoped<IECommerceDbContext>(sp => new ECommerceDbContext(sp, ConnectionStringSectionName));
    }


    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
    }
}

