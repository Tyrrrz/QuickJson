namespace QuickJson;

/// <summary>
/// Represents a JSON string value.
/// </summary>
public class JsonString(string value) : JsonNode
{
    /// <summary>
    /// Node's value as a string.
    /// </summary>
    public string Value { get; } = value;

    /// <inheritdoc />
    public override string TryGetString() => Value;
}
