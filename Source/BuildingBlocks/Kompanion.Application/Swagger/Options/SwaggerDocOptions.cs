namespace Kompanion.Application.Swagger.Options;

public class SwaggerDocOptions
{
    public bool XmlDocEnabled { get; private set; }

    private string documentName;

    public string DocumentName
    {
        get => documentName;
        set
        {
            documentName = value;

            if (!string.IsNullOrWhiteSpace(value))
            {
                XmlDocEnabled = true;
            }
        }
    }
}
