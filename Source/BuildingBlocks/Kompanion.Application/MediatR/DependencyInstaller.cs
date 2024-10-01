using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR.Pipeline;
using Kompanion.Application.MediatR.Behaviors;

namespace Kompanion.Application.MediatR;

public static class DependencyInstaller
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        Assembly[] assemblies = { assembly, Assembly.GetExecutingAssembly() };
        services.AddMediatR(assemblies);

        return services;
    }

    private static void AddMediatR(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies);

        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionBehavior<,,>));

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assemblies);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(PerformanceBehavior<,>));
            //configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
        });
    }
}