using Asp.Versioning;

namespace Kompanion.Application.ApiVersion.Options;

public sealed class ApiVersioningOptions
{
    public bool AssumeDefaultVersionWhenUnspecified { get; set; } = true;
    public string DefaultApiVersion { get; set; } = "1.0";
    public bool ReportApiVersions { get; set; } = true;
    public bool EnableVersionedApiExplorer { get; set; } = true;
    public IEnumerable<IApiVersionReader> ApiVersionReaders { get; set; } = new List<IApiVersionReader>();
}