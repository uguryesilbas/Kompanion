using Elastic.Clients.Elasticsearch;
using Elastic.CommonSchema.Serilog;
using Elastic.Ingest.Elasticsearch;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Serilog;
using Serilog.Events;
using Kompanion.Infrastructure.Logging.Abstractions;
using Kompanion.Infrastructure.Logging.Options;

namespace Kompanion.Infrastructure.Logging.Configurations;

internal sealed class ElasticsearchConfiguration : BaseLogConfiguration
{
    protected override void ConfigureSink(LoggerConfiguration loggerConfiguration, LoggingOptions loggingOptions)
    {
        ElasticsearchClientSettings clientSettings = ConfigureElasticsearchClientSetting(loggingOptions);

        ElasticsearchClient elasticsearchClient = new(clientSettings);

        ElasticsearchSinkOptions sinkOptions = new ElasticsearchSinkOptions(elasticsearchClient.Transport)
        {
            BootstrapMethod = BootstrapMethod.Failure,
            MinimumLevel = LogEventLevel.Information,
            TextFormatting = new EcsTextFormatterConfiguration(),
        };

        loggerConfiguration.WriteTo.Elasticsearch(sinkOptions);
    }

    private static ElasticsearchClientSettings ConfigureElasticsearchClientSetting(LoggingOptions loggingOptions)
    {
        ElasticsearchOptions elasticsearchOptions = loggingOptions.ElasticsearchOptions;

        Uri address = new Uri(elasticsearchOptions.Address);

        ElasticsearchClientSettings clientSettings = new ElasticsearchClientSettings(address)
            .DefaultIndex(elasticsearchOptions.Index);

        if (!elasticsearchOptions.UseAuthentication)
        {
            return clientSettings;
        }

        BasicAuthentication basicAuthentication = new(elasticsearchOptions.UserName, elasticsearchOptions.Password);

        clientSettings.Authentication(basicAuthentication);

        return clientSettings;
    }
}