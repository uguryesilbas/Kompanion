using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Kompanion.Application.Swagger.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kompanion.Application.Swagger.Configurations;

internal class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly SwaggerOptions _swaggerOptions;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, SwaggerOptions swaggerOptions)
    {
        _provider = provider;
        _swaggerOptions = swaggerOptions;
    }

    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        if (_provider?.ApiVersionDescriptions is not null)
        {
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionApiInfo(description));
            }
        }

        if (_swaggerOptions.DocOptions is null || !_swaggerOptions.DocOptions.XmlDocEnabled)
        {
            return;
        }

        string fileExtension = Path.GetExtension(_swaggerOptions.DocOptions.DocumentName);

        if (string.IsNullOrWhiteSpace(fileExtension) || !fileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("XMLDocFile extension must be .xml");
        }

        string filePath = Path.Combine(AppContext.BaseDirectory, _swaggerOptions.DocOptions.DocumentName);

        if (!File.Exists(filePath))
        {
            throw new ArgumentException($"XMLDocFile could not found in {_swaggerOptions.DocOptions.DocumentName}");
        }

        options.IncludeXmlComments(filePath);
    }

    private OpenApiInfo CreateVersionApiInfo(ApiVersionDescription description)
    {
        OpenApiInfo openApiInfo = new OpenApiInfo
        {
            Title = _swaggerOptions.ProjectName,
            Version = description.ApiVersion.ToString(),
        };

        if (description.IsDeprecated)
        {
            openApiInfo.Description += "This API has been deprecated";
        }

        return openApiInfo;
    }
}