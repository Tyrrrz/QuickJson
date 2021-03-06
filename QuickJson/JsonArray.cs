using System.Collections.Generic;

#nullable enable
namespace QuickJson;

internal class JsonArray : JsonNode
{
    public JsonNode[] Children { get; }

    public JsonArray(JsonNode[] children) => Children = children;

    public override IEnumerable<JsonNode> EnumerateChildren() => Children;
}
#nullable restore