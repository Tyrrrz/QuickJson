namespace QuickJson;

/// <summary>
/// Represents a JSON number value.
/// </summary>
public class JsonNumber(double value) : JsonNode
{
    /// <summary>
    /// Node's value as a double.
    /// </summary>
    public double Value { get; } = value;

    /// <inheritdoc />
    public override double? TryGetNumber() => Value;
}
