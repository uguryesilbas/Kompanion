using Microsoft.Extensions.DependencyInjection;
using Kompanion.Application.Exceptions.Filters;

namespace Kompanion.Application.Exceptions
{
    public static class DependencyInstaller
    {
        public static IServiceCollection AddExceptionFilter(this IServiceCollection services)
        {
            services.AddControllers(opt => opt.Filters.Add<ExceptionFilter>());

            return services;
        }
    }
}
