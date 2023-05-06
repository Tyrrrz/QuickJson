#nullable enable
using System.Collections.Generic;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonObject : JsonNode
{
    public JsonProperty[] Properties { get; }

    public JsonObject(JsonProperty[] properties) => Properties = properties;

    public override IEnumerable<JsonProperty> EnumerateProperties() => Properties;
}