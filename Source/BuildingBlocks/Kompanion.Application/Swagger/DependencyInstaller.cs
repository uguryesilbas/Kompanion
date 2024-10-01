using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Kompanion.Application.Swagger.Configurations;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using Kompanion.Application.Swagger.OperationFilters;
using Kompanion.Application.Swagger.Options;

namespace Kompanion.Application.Swagger;

public static class DependencyInstaller
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerOptions> swaggerAction = null)
    {
        SwaggerOptions swaggerOptions = new();
        swaggerAction?.Invoke(swaggerOptions);

        services.AddSingleton(swaggerOptions);

        services.AddSwaggerGen(opt =>
        {
            if (swaggerOptions.JsonIgnoreEnable)
            {
                opt.OperationFilter<JsonIgnoreOperationFilter>();
            }

            if (swaggerOptions.Headers is { Count: > 0 })
            {
                swaggerOptions.Headers.ForEach(header =>
                {
                    opt.OperationFilter<HeaderParameterOperationFilter>(header);
                });
            }

            if (swaggerOptions.BearerOptions?.AuthEnable == true)
            {
                opt.AddSecurityDefinition(swaggerOptions.BearerOptions.HeaderKey, new OpenApiSecurityScheme
                {
                    Description = swaggerOptions.BearerOptions.BearerDescription,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = ApplicationConstants.SwaggerConstants.BearerFormat,
                    Scheme = swaggerOptions.BearerOptions.HeaderKey
                });

                opt.OperationFilter<AuthenticationRequirementsOperationFilter>(swaggerOptions.BearerOptions);
            }
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
    {
        IApiVersionDescriptionProvider apiVersionProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
        SwaggerOptions swaggerOptions = app.ApplicationServices.GetRequiredService<SwaggerOptions>();

        app.UseSwagger(x => { });

        app.UseSwaggerUI(opt =>
        {
            opt.DefaultModelExpandDepth(2);
            opt.DefaultModelRendering(ModelRendering.Model);
            opt.DefaultModelsExpandDepth(-1);
            opt.DisplayRequestDuration();
            opt.DocExpansion(DocExpansion.List);
            opt.EnableDeepLinking();
            opt.EnableFilter();
            opt.ShowExtensions();
            opt.ShowCommonExtensions();
            opt.EnableValidator();

            opt.SetSwaggerEndpoint(swaggerOptions, apiVersionProvider);
        });

        return app;
    }

    private static void SetSwaggerEndpoint(this SwaggerUIOptions swaggerUiOptions, SwaggerOptions swaggerOptions, IApiVersionDescriptionProvider apiVersionProvider = null)
    {
        if (apiVersionProvider is null)
        {
            swaggerUiOptions.SwaggerEndpoint(ApplicationConstants.SwaggerConstants.DefaultSwaggerEndpoint, ApplicationConstants.SwaggerConstants.DefaultSwaggerApiVersion);
            return;
        }

        foreach (ApiVersionDescription description in apiVersionProvider.ApiVersionDescriptions)
        {
            string swaggerFileUrl = $"/{ApplicationConstants.SwaggerConstants.DefaultSwaggerEndpointFileDirectory}/{description.GroupName}/{ApplicationConstants.SwaggerConstants.DefaultSwaggerEndpointFileName}";
            string name = $"{swaggerOptions.ProjectName} {description.GroupName.ToUpperInvariant()}";
            swaggerUiOptions.SwaggerEndpoint(swaggerFileUrl, name);
        }
    }
}