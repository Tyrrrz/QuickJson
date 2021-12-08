using System.Collections.Generic;

#nullable enable
namespace QuickJson;

internal class JsonObject : JsonNode
{
    public JsonProperty[] Properties { get; }

    public JsonObject(JsonProperty[] properties) => Properties = properties;

    public override IEnumerable<JsonProperty> EnumerateProperties() => Properties;
}
#nullable restore