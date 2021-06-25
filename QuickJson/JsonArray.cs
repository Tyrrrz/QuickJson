using System.Collections.Generic;

namespace QuickJson
{
    public class JsonArray : JsonNode
    {
        public JsonNode[] Children { get; }

        public JsonArray(JsonNode[] children) => Children = children;

        public override IEnumerable<JsonNode> EnumerateChildren() => Children;
    }
}