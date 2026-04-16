#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE
[ExcludeFromCodeCoverage]
#endif
internal partial class JsonString(string value) : JsonNode
{
    public string Value { get; } = value;

    public override string TryGetString() => Value;
}
