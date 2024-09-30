using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace Kompanion.Application.Swagger.OperationFilters;

internal class JsonIgnoreOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        IList<ApiParameterDescription> parameterDescriptions = context.ApiDescription.ParameterDescriptions;

        foreach (ApiParameterDescription parameterDescription in parameterDescriptions)
        {
            bool? jsonIgnore = parameterDescription.ModelMetadata is DefaultModelMetadata metadata
                ? metadata.Attributes.PropertyAttributes?.Any(x => x is JsonIgnoreAttribute)
                : false;

            if (jsonIgnore.HasValue && jsonIgnore.Value)
            {
                OpenApiParameter apiParameter = operation.Parameters.FirstOrDefault(x => x.Name == parameterDescription.Name);

                if (apiParameter is not null)
                {
                    operation.Parameters.Remove(apiParameter);
                }
            }
        }
    }
}