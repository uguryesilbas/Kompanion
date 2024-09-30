using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Kompanion.Application.Extensions;
using Kompanion.Application.Serializations.Json;

namespace Kompanion.Application.Serializations;

public static class DependencyInstaller
{
    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        JsonSerializerOptions jsonSerializerOptions = JsonSerialization.JsonSerializerOptions;

        services.Configure<JsonOptions>(configure =>
        {
            configure.JsonSerializerOptions.ReadCommentHandling = jsonSerializerOptions.ReadCommentHandling;

            configure.JsonSerializerOptions.AllowTrailingCommas = jsonSerializerOptions.AllowTrailingCommas;

            configure.JsonSerializerOptions.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;

            configure.JsonSerializerOptions.DefaultIgnoreCondition = jsonSerializerOptions.DefaultIgnoreCondition;

            configure.JsonSerializerOptions.Converters.AddRange(jsonSerializerOptions.Converters);
        });

        return services;
    }
}