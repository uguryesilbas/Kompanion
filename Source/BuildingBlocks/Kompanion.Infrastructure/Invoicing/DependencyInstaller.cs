using Kompanion.Infrastructure.Invoicing.Abstractions;
using Kompanion.Infrastructure.Invoicing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kompanion.Infrastructure.Invoicing;

public static class DependencyInstaller
{
    public static IServiceCollection AddInvoiceService(this IServiceCollection services)
    {
        services.AddTransient<IInvoiceService, InvoiceService>();

        return services;
    }
}

