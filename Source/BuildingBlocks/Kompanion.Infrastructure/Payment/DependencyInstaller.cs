using Kompanion.Application.Abstractions;
using Kompanion.Application.Enums;
using Kompanion.Infrastructure.Payment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kompanion.Infrastructure.Payment;

public static class DependencyInstaller
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        services.AddKeyedTransient<IPaymentService, ABankPaymentService>(PaymentBankType.ABank);

        services.AddKeyedTransient<IPaymentService, YBankPaymentService>(PaymentBankType.YBank);

        return services;
    }
}

