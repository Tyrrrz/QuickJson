#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonBool(bool value) : JsonNode
{
    public bool Value { get; } = value;

    public override bool? TryGetBool() => Value;
}

internal partial class JsonBool
{
    public static JsonBool True { get; } = new(true);
    public static JsonBool False { get; } = new(false);
}
