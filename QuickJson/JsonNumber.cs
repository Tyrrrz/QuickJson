#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonNumber : JsonNode
{
    public double Value { get; }

    public JsonNumber(double value) => Value = value;

    public override double? TryGetNumber() => Value;
}