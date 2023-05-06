#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonNull : JsonNode
{
    public static JsonNull Instance { get; } = new();
}