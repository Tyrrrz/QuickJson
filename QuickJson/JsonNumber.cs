#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonNumber(double value) : JsonNode
{
    public double Value { get; } = value;

    public override double? TryGetNumber() => Value;
}
