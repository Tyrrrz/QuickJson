#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE
[ExcludeFromCodeCoverage]
#endif
internal partial class JsonArray(JsonNode[] children) : JsonNode
{
    public JsonNode[] Children { get; } = children;

    public override IEnumerable<JsonNode> EnumerateChildren() => Children;
}
