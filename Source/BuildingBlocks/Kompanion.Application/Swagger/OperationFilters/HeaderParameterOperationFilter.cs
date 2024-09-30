using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Kompanion.Application.Swagger.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kompanion.Application.Swagger.OperationFilters;

internal class HeaderParameterOperationFilter(SwaggerHeaderOptions headerOptions) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = headerOptions.Name,
            In = ParameterLocation.Header,
            Required = headerOptions.Required,
            Schema = new OpenApiSchema
            {
                Type = nameof(String),
                Default = new OpenApiString(headerOptions.DefaultValue)
            }
        });
    }
}
