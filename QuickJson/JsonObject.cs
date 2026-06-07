using System.Collections.Generic;

namespace QuickJson;

/// <summary>
/// Represents a JSON object value.
/// </summary>
public class JsonObject(JsonProperty[] properties) : JsonNode
{
    /// <summary>
    /// Object's properties.
    /// </summary>
    public JsonProperty[] Properties { get; } = properties;

    /// <inheritdoc />
    public override IEnumerable<JsonProperty> EnumerateProperties() => Properties;
}
