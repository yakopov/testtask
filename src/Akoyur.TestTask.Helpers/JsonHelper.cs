using System.Text.Json;

namespace Akoyur.TestTask.Helpers;

/// <summary>
/// Helper class for JSON serialization.
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Default JSON serializer options with camel case property naming and indentation.
    /// </summary>
    static JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    /// <summary>
    /// Serializes an object to a JSON string using predefined options.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A JSON string representing the object.</returns>
    public static string ToJson(object obj) => JsonSerializer.Serialize(obj, _options);
}

