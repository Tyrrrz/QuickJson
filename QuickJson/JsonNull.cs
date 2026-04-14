#nullable enable
#if !QUICKJSON_INCLUDE_COVERAGE && (NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER || NET5_0_OR_GREATER)
using System.Diagnostics.CodeAnalysis;
#endif

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE && (NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER || NET5_0_OR_GREATER)
[ExcludeFromCodeCoverage]
#endif
internal partial class JsonNull : JsonNode
{
    public static JsonNull Instance { get; } = new();
}
