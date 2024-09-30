namespace Kompanion.Infrastructure.Logging.Options;

public sealed class GraylogOptions
{
    public string Address { get; init; }
    public int Port { get; init; }
    public string Facility { get; init; }
    public bool UseSsl { get; init; }
}