using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using ApiVersioningOptions = Kompanion.Application.ApiVersion.Options.ApiVersioningOptions;

namespace Kompanion.Application.ApiVersion;

public static class DependencyInstaller
{
    private const string GroupNameFormat = "'v'VVV";

    public static IServiceCollection AddApiVersion(this IServiceCollection services, Action<ApiVersioningOptions> options = null)
    {
        ApiVersioningOptions versioningOptions = new();

        options?.Invoke(versioningOptions);

        services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = DefaultVersionParsing(versioningOptions.DefaultApiVersion);

                opt.ReportApiVersions = versioningOptions.ReportApiVersions;

                opt.AssumeDefaultVersionWhenUnspecified = versioningOptions.AssumeDefaultVersionWhenUnspecified;

                opt.ApiVersionReader = ApiVersionReader.Combine(ApiVersionReaders(versioningOptions.ApiVersionReaders.ToArray()));

            })
            .AddApiExplorer(opt =>
            {
                if (!versioningOptions.EnableVersionedApiExplorer)
                    return;

                opt.GroupNameFormat = GroupNameFormat;
                opt.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    private static Asp.Versioning.ApiVersion DefaultVersionParsing(string defaultApiVersion)
    {
        if (string.IsNullOrWhiteSpace(defaultApiVersion))
        {
            return Asp.Versioning.ApiVersion.Default;
        }

        bool validVersion = ApiVersionParser.Default.TryParse(defaultApiVersion.Replace("v", ""), out Asp.Versioning.ApiVersion defaultVersion);

        return validVersion ? defaultVersion : Asp.Versioning.ApiVersion.Default;
    }

    private static IEnumerable<IApiVersionReader> ApiVersionReaders(IApiVersionReader[] readers)
    {
        if (readers == null || readers.Length == 0)
        {
            readers = new IApiVersionReader[]
            {
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"),
                new QueryStringApiVersionReader("x-api-version")
            };
        }

        return readers;
    }
}