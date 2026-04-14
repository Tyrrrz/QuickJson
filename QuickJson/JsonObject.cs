#nullable enable
using System.Collections.Generic;
#if !QUICKJSON_INCLUDE_COVERAGE
using System.Diagnostics.CodeAnalysis;
#endif

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE
[ExcludeFromCodeCoverage]
#endif
internal partial class JsonObject(JsonProperty[] properties) : JsonNode
{
    public JsonProperty[] Properties { get; } = properties;

    public override IEnumerable<JsonProperty> EnumerateProperties() => Properties;
}
