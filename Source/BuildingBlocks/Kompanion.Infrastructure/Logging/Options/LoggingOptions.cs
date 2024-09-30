using Kompanion.Infrastructure.Logging.Enums;

namespace Kompanion.Infrastructure.Logging.Options;

public sealed record LoggingOptions
{
    public LogType LogType { get; init; } = LogType.ElasticSearch;
    public bool WriteConsole { get; init; }
    public bool WriteDebug { get; init; }
    public ElasticsearchOptions ElasticsearchOptions { get; init; } = new();
    public GraylogOptions GraylogOptions { get; init; } = new();
}