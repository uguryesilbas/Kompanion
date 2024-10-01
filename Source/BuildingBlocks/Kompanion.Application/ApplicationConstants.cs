namespace Kompanion.Application;

public static class ApplicationConstants
{
    public static class SwaggerConstants
    {
        public const string DefaultSwaggerEndpointFileName = "swagger.json";
        public const string DefaultSwaggerEndpointFileDirectory = "swagger";
        public const string DefaultSwaggerEndpoint = $"/{DefaultSwaggerEndpointFileDirectory}/v1/{DefaultSwaggerEndpointFileName}";
        public const string DefaultSwaggerApiVersion = "1";

        public const string BearerFormat = "JWT";
        public const string BearerHeaderKey = "Bearer";

        public static string DefaultBearerDescription => $"JWT Authorization header using the {BearerHeaderKey} scheme. \r\n\r\n Enter your token in the text input below.";

    }

    public static class ConfigurationSectionConstants
    {
        public const string RedisSection = "Redis";
        public const string LogSection = "Logger";
    }

    public static class ApiVersioningConstants
    {
        public const string DefaultApiVersion = "1.0";
    }
}

