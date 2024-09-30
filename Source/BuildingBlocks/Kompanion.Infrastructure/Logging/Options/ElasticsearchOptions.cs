namespace Kompanion.Infrastructure.Logging.Options;

public sealed record ElasticsearchOptions
{
    public string Address { get; init; }
    public string Index { get; init; }
    public bool UseAuthentication { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
}