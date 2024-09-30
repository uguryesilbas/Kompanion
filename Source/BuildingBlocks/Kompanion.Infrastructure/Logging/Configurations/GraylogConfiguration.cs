using Serilog;
using Serilog.Events;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;
using Kompanion.Infrastructure.Logging.Abstractions;
using Kompanion.Infrastructure.Logging.Options;

namespace Kompanion.Infrastructure.Logging.Configurations;

internal class GraylogConfiguration : BaseLogConfiguration
{
    protected override void ConfigureSink(LoggerConfiguration loggerConfiguration, LoggingOptions loggingOptions)
    {
        GraylogOptions graylogOptions = loggingOptions.GraylogOptions;

        GraylogSinkOptions sinkOptions = new()
        {
            HostnameOrAddress = graylogOptions.Address,
            Port = graylogOptions.Port,
            Facility = graylogOptions.Facility,
            UseSsl = graylogOptions.UseSsl,
            MinimumLogEventLevel = LogEventLevel.Information,
            TransportType = TransportType.Tcp,
            UseGzip = true,
        };

        loggerConfiguration.WriteTo.Graylog(sinkOptions);
    }
}