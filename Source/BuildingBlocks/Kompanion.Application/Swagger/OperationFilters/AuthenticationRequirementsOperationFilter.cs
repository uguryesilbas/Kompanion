using Microsoft.OpenApi.Models;
using Kompanion.Application.Swagger.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kompanion.Application.Swagger.OperationFilters;

internal class AuthenticationRequirementsOperationFilter(SwaggerAuthOptions authOptions) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = authOptions.HeaderKey
            }
        };

        OpenApiSecurityRequirement requirement = new OpenApiSecurityRequirement
            {
                {securityScheme, new List<string>() }
            };

        operation.Security.Add(requirement);
    }
}

