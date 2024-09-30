using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kompanion.Application.Serializations.Json;

public static class JsonSerialization
{
    private static readonly object LockingObject = new();

    private static JsonSerializerOptions _jsonSerializerOptions;
    internal static JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            lock (LockingObject)
            {
                return _jsonSerializerOptions ??= new JsonSerializerOptions
                {
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                    Converters = { new JsonStringEnumConverter() }
                };
            }
        }
    }

    public static string Serialize<T>(T obj, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Serialize(obj, options ?? JsonSerializerOptions);
    }

    public static T Deserialize<T>(string json, JsonSerializerOptions options = null)
    {
        return string.IsNullOrWhiteSpace(json)
            ? default
            : JsonSerializer.Deserialize<T>(json, options ?? JsonSerializerOptions);
    }
}