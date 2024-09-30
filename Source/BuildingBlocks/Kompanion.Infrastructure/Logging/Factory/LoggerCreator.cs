using Serilog;
using Kompanion.Infrastructure.Logging.Configurations;
using Kompanion.Infrastructure.Logging.Enums;
using Kompanion.Infrastructure.Logging.Options;

namespace Kompanion.Infrastructure.Logging.Factory;

internal static class LoggerCreator
{
    public static LoggerConfiguration Create(LoggingOptions options)
    {
        return options.LogType switch
        {
            LogType.None => new LoggerConfiguration().WriteTo.Console(),
            LogType.ElasticSearch => new ElasticsearchConfiguration().LogConfiguration(options),
            LogType.Graylog => new GraylogConfiguration().LogConfiguration(options),
            _ => throw new NotImplementedException("Logger type not implemented!")
        };
    }
}