using System.Text;

namespace QuickJson;

/// <summary>
/// Provides static methods for parsing JSON documents from strings.
/// </summary>
public static class Json
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

    /// <summary>
    /// Escapes special JSON characters in the specified string.
    /// </summary>
    public static string Escape(string value)
    {
        var buffer = new StringBuilder(value.Length);

        foreach (var ch in value)
        {
            switch (ch)
            {
                case '"':
                    buffer.Append("\\\"");
                    break;
                case '\\':
                    buffer.Append("\\\\");
                    break;
                case '\b':
                    buffer.Append("\\b");
                    break;
                case '\f':
                    buffer.Append("\\f");
                    break;
                case '\n':
                    buffer.Append("\\n");
                    break;
                case '\r':
                    buffer.Append("\\r");
                    break;
                case '\t':
                    buffer.Append("\\t");
                    break;
                default:
                    // Escape other control characters using \uXXXX
                    if (ch < 0x20)
                        buffer.Append("\\u").Append(((int)ch).ToString("x4"));
                    else
                        buffer.Append(ch);
                    break;
            }
        }

        return buffer.ToString();
    }
}
