#nullable enable
using System.Collections.Generic;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonObject(JsonProperty[] properties) : JsonNode
{
    public JsonProperty[] Properties { get; } = properties;

    public override IEnumerable<JsonProperty> EnumerateProperties() => Properties;
}
