#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonString(string value) : JsonNode
{
    public string Value { get; } = value;

    public override string TryGetString() => Value;
}
