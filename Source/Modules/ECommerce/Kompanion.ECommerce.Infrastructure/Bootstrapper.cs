using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Kompanion.ECommerce.Infrastructure.Context;
using Kompanion.ECommerce.Infrastructure.Domain;
using Kompanion.ECommerce.Domain.Product;
using Kompanion.Infrastructure.Caching;
using Kompanion.Application;
using Kompanion.ECommerce.Domain.Order;
using Kompanion.Infrastructure.Payment;
using Kompanion.ECommerce.Domain.PaymentRule;
using Kompanion.Infrastructure.Invoicing;
using Kompanion.Infrastructure.Notification;
using Kompanion.ECommerce.Infrastructure.Saga.Order;
using Kompanion.Infrastructure.Logging;

namespace Kompanion.ECommerce.Infrastructure;

public static class Bootstrapper
{
    public static WebApplicationBuilder AddECommerceInfrastructures(this WebApplicationBuilder builder)
    {
        builder.AddSerilog(ApplicationConstants.ConfigurationSectionConstants.LogSection);

        builder.Services.AddDistributedCache(ApplicationConstants.ConfigurationSectionConstants.RedisSection);

        builder.Services.AddDbContext();

        builder.Services.AddRepositories();

        builder.Services.AddServices();


        return builder;
    }

    private static void AddDbContext(this IServiceCollection services)
    {
        const string ConnectionStringSectionName = "ECommerceApi";

        services.AddScoped<IECommerceDbContext>(sp => new ECommerceDbContext(sp, ConnectionStringSectionName));
    }


    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IPaymentRuleRepository, PaymentRuleRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddPaymentService();

        services.AddInvoiceService();

        services.AddNotifacationService();

        services.AddTransient<IOrderProcessSagaService, OrderProcessSagaService>();
    }
}

