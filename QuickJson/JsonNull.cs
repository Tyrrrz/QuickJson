namespace QuickJson;

/// <summary>
/// Represents a JSON null value.
/// </summary>
public class JsonNull : JsonNode
{
    /// <summary>
    /// Singleton instance representing the JSON null value.
    /// </summary>
    public static JsonNull Instance { get; } = new();
}
