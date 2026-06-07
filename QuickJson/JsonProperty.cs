namespace QuickJson;

/// <summary>
/// Represents a JSON property, which is a key-value pair within a JSON object.
/// </summary>
public class JsonProperty(string name, JsonNode value)
{
    /// <summary>
    /// Property's name (key).
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Property's value.
    /// </summary>
    public JsonNode Value { get; } = value;
}
