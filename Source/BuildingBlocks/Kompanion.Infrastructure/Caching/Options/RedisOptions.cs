using Kompanion.Application.Extensions;

namespace Kompanion.Infrastructure.Caching.Options;

public class RedisOptions
{
    private static readonly int Timeout = TimeSpan.FromSeconds(5).TotalMilliseconds.As<int>();

    public bool Enabled { get; set; }
    public List<string> Endpoints { get; set; } = new();
    public string Password { get; set; }
    public bool AbortOnConnectFail { get; set; } = true;
    public int AsyncTimeout { get; set; } = Timeout;
    public int ConnectTimeout { get; set; } = Timeout;
    public int SyncTimeout { get; set; } = Timeout;
    public int ConnectRetry { get; set; } = 3;
    public int DefaultDatabase { get; set; } = 0;
}