#nullable enable
using System.Collections.Generic;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonArray : JsonNode
{
    public JsonNode[] Children { get; }

    public JsonArray(JsonNode[] children) => Children = children;

    public override IEnumerable<JsonNode> EnumerateChildren() => Children;
}