namespace Kompanion.Application.Swagger.Options;

public class SwaggerAuthOptions
{
    public bool AuthEnable { get; set; }
    public string HeaderKey { get; set; } = ApplicationConstants.SwaggerConstants.BearerHeaderKey;
    public string BearerDescription { get; set; } = ApplicationConstants.SwaggerConstants.DefaultBearerDescription;
}

