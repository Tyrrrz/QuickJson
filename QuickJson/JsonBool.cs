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
internal partial class JsonBool(bool value) : JsonNode
{
    public bool Value { get; } = value;

    public override bool? TryGetBool() => Value;
}

internal partial class JsonBool
{
    public static JsonBool True { get; } = new(true);
    public static JsonBool False { get; } = new(false);
}
