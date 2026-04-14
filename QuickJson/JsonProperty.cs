#nullable enable
#if !QUICKJSON_INCLUDE_COVERAGE
using System.Diagnostics.CodeAnalysis;
#endif

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE
[ExcludeFromCodeCoverage]
#endif
internal partial class JsonProperty(string name, JsonNode value)
{
    public string Name { get; } = name;

    public JsonNode Value { get; } = value;
}
