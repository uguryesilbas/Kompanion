using Kompanion.Application.Abstractions;
using Kompanion.Infrastructure.Notification.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kompanion.Infrastructure.Notification;

public static class DependencyInstaller
{
    public static IServiceCollection AddNotifacationService(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
