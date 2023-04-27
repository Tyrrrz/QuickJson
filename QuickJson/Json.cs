#nullable enable
namespace QuickJson;

internal static class Json
{
    /// <summary>
    /// Attempts to parse the specified string as a JSON document.
    /// Returns null in case of failure.
    /// </summary>
    public static JsonNode? TryParse(string source) => new JsonReader(source).TryReadDocument();

    /// <summary>
    /// Parses the specified string as a JSON document.
    /// </summary>
    public static JsonNode Parse(string source) => new JsonReader(source).ReadDocument();
}