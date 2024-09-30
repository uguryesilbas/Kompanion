namespace Kompanion.Application.Swagger.Options;

public class SwaggerOptions
{
    public string ProjectName { get; set; }
    public bool JsonIgnoreEnable { get; set; } = true;

    public SwaggerAuthOptions BearerOptions { get; set; }
    public SwaggerDocOptions DocOptions { get; set; }
    public List<SwaggerHeaderOptions> Headers { get; set; } = new();
}

