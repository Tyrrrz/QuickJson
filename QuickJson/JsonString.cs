#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonString : JsonNode
{
    public string Value { get; }

    public JsonString(string value) => Value = value;

    public override string TryGetString() => Value;
}