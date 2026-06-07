namespace QuickJson;

/// <summary>
/// Represents a JSON boolean value.
/// </summary>
public partial class JsonBool(bool value) : JsonNode
{
    /// <summary>
    /// Node's value as a boolean.
    /// </summary>
    public bool Value { get; } = value;

    /// <inheritdoc />
    public override bool? TryGetBool() => Value;
}

public partial class JsonBool
{
    /// <summary>
    /// Singleton instance representing the JSON boolean value `true`.
    /// </summary>
    public static JsonBool True { get; } = new(true);

    /// <summary>
    /// Singleton instance representing the JSON boolean value `false`.
    /// </summary>
    public static JsonBool False { get; } = new(false);
}
