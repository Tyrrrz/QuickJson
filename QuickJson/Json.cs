#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
#if !QUICKJSON_INCLUDE_COVERAGE
[ExcludeFromCodeCoverage]
#endif
internal static partial class Json
{
    /// <summary>
    /// Attempts to parse the specified string as a JSON document.
    /// Returns null in case of failure.
    /// </summary>
    public static JsonNode? TryParse(string source) => new JsonReader(source).TryReadDocument();

    /// <summary>
    /// Parses the specified string as a JSON document.
    /// </summary>
    public static JsonNode Parse(string source) => new JsonReader(source).ReadDocument();
}
