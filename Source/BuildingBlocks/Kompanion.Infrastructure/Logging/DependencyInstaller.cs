using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Kompanion.Application.Extensions;
using Kompanion.Infrastructure.Logging.Factory;
using Kompanion.Infrastructure.Logging.Options;

namespace Kompanion.Infrastructure.Logging;

public static class DependencyInstaller
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, string loggerSectionName)
    {
        builder.Logging.ClearProviders();

        LoggingOptions loggingOptions = builder.Services.GetOptions<LoggingOptions>(loggerSectionName);

        ArgumentNullException.ThrowIfNull(loggingOptions, "Logging options cannot be null!");

        LoggerConfiguration loggerConfiguration = LoggerCreator.Create(loggingOptions);

        builder.Host.UseSerilog(loggerConfiguration.CreateLogger());

        return builder;
    }
}