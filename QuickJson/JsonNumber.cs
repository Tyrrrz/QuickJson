#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE
[ExcludeFromCodeCoverage]
#endif
internal partial class JsonNumber(double value) : JsonNode
{
    public double Value { get; } = value;

    public override double? TryGetNumber() => Value;
}
