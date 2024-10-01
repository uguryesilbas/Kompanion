using Kompanion.Application.Swagger;
using Kompanion.ECommerce.Application;
using Kompanion.ECommerce.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddECommerceApplications();

builder.AddECommerceInfrastructures();

WebApplication app = builder.Build();

app.UseSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
