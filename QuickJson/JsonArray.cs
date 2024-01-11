#nullable enable
using System.Collections.Generic;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonArray(JsonNode[] children) : JsonNode
{
    public JsonNode[] Children { get; } = children;

    public override IEnumerable<JsonNode> EnumerateChildren() => Children;
}
