using System.Collections.Generic;

namespace QuickJson;

/// <summary>
/// Represents a JSON array value.
/// </summary>
public class JsonArray(JsonNode[] children) : JsonNode
{
    /// <summary>
    /// Array's children nodes.
    /// </summary>
    public JsonNode[] Children { get; } = children;

    /// <inheritdoc />
    public override IEnumerable<JsonNode> EnumerateChildren() => Children;
}
