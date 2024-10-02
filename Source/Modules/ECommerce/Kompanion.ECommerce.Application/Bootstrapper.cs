using Microsoft.AspNetCore.Builder;
using Kompanion.Application.MediatR;
using System.Reflection;
using Kompanion.Application.Exceptions;
using Kompanion.Application.ApiVersion;
using Kompanion.Application.Swagger;
using Kompanion.Application.Swagger.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kompanion.ECommerce.Application;

public static class Bootstrapper
{
    private const string ProjectName = "Kompanion.ECommerce.API";

    public static WebApplicationBuilder AddECommerceApplications(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddExceptionFilter();

        builder.Services.AddApiVersion();

        builder.Services.AddMediatR(Assembly.GetAssembly(typeof(Bootstrapper)));

        builder.Services.AddSwagger(opt =>
        {
            opt.ProjectName = ProjectName;
            opt.BearerOptions = new SwaggerAuthOptions { AuthEnable = true };
            opt.DocOptions = new SwaggerDocOptions { DocumentName = $"{ProjectName}.xml" };
        });

        return builder;
    }

    private static void AddAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "ECommerce",
                    ValidAudience = "ECommerce",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KAhOpbxKPNLK03DuQqq1pfXB3tKZQ8rc"))
                };
            });
    }
}

