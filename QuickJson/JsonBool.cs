#nullable enable
namespace QuickJson;

internal class JsonBool : JsonNode
{
    public static JsonBool True { get; } = new(true);
    public static JsonBool False { get; } = new(false);

    public bool Value { get; }

    public JsonBool(bool value) => Value = value;

    public override bool? TryGetBool() => Value;
}