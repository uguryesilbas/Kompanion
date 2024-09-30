using Serilog;
using Serilog.Exceptions;
using Kompanion.Infrastructure.Logging.Options;

namespace Kompanion.Infrastructure.Logging.Abstractions;

internal abstract class BaseLogConfiguration
{
    protected abstract void ConfigureSink(LoggerConfiguration loggerConfiguration, LoggingOptions loggingOptions);

    internal virtual LoggerConfiguration LogConfiguration(LoggingOptions loggingOptions)
    {
        LoggerConfiguration loggerConfiguration = CreateBaseLoggerConfiguration();

        AddOptionalConsoleAndDebug(loggerConfiguration, loggingOptions);

        ConfigureSink(loggerConfiguration, loggingOptions);

        return loggerConfiguration;
    }

    private static LoggerConfiguration CreateBaseLoggerConfiguration()
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithCorrelationId()
            .Enrich.WithClientIp()
            .Enrich.WithEnvironmentName()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMemoryUsage();
    }

    private static void AddOptionalConsoleAndDebug(LoggerConfiguration loggerConfiguration, LoggingOptions loggingOptions)
    {
        if (loggingOptions.WriteConsole)
            loggerConfiguration.WriteTo.Console();

        if (loggingOptions.WriteDebug)
            loggerConfiguration.WriteTo.Debug();
    }
}