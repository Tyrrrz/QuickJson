using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace QuickJson;

internal class JsonReader
{
    private readonly string _source;
    private int _index;

    public JsonReader(string source) => _source = source;

    private char? TryRead(Func<char, bool> predicate)
    {
        if (_index < _source.Length)
        {
            var c = _source[_index];

            if (predicate(c))
            {
                _index++;
                return c;
            }

            return null;
        }

        return null;
    }

    private bool TryRead(char expectedChar) => TryRead(x => x == expectedChar) is not null;

    private string? TryRead(int length, Func<string, bool> predicate)
    {
        if (_index + length <= _source.Length)
        {
            var sub = _source.Substring(_index, length);

            if (predicate(sub))
            {
                _index += length;
                return sub;
            }

            return null;
        }

        return null;
    }

    private bool TryRead(string expectedString) => TryRead(
        expectedString.Length,
        s => string.Equals(s, expectedString, StringComparison.Ordinal)
    ) is not null;

    private void SkipWhiteSpace()
    {
        while (TryRead(char.IsWhiteSpace) is not null)
        {
            // Nom nom
        }
    }

    private JsonNode? TryReadNull() =>
        TryRead("null")
            ? JsonNull.Instance
            : null;

    private JsonNode? TryReadBool()
    {
        JsonNode? TryReadBoolTrue() =>
            TryRead("true")
                ? JsonBool.True
                : null;

        JsonNode? TryReadBoolFalse() =>
            TryRead("false")
                ? JsonBool.False
                : null;

        return TryReadBoolTrue() ?? TryReadBoolFalse();
    }

    private JsonNode? TryReadNumber()
    {
        var buffer = new StringBuilder();

        // Collect digit characters and decimal separator character
        while (TryRead(x => char.IsDigit(x) || x == '.') is { } c)
            buffer.Append(c);

        return double.TryParse(buffer.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out var value)
            ? new JsonNumber(value)
            : null;
    }

    private JsonNode? TryReadString()
    {
        char? TryReadEscape()
        {
            var indexCheckpoint = _index;

            if (!TryRead('\\'))
                return null;

            if (TryRead(x => x is '\\' or '"' or '/' or 'b' or 'f' or 'n' or 'r' or 't' or 'u') is { } c)
            {
                // Unicode character escape
                if (c == 'u')
                {
                    var hexSequence = TryRead(4, s => Regex.IsMatch(s, @"^[0-9a-fA-F]{4}$"));

                    if (hexSequence is not null && short.TryParse(
                            hexSequence,
                            NumberStyles.AllowHexSpecifier,
                            CultureInfo.InvariantCulture,
                            out var codepoint))
                    {
                        return (char) codepoint;
                    }

                    // Backtrack to the beginning to consume the characters raw
                    _index = indexCheckpoint;
                    return null;
                }

                // Basic escape
                return c switch
                {
                    'b' => '\b',
                    'f' => '\f',
                    'n' => '\n',
                    'r' => '\r',
                    't' => '\t',
                    _ => c
                };
            }

            // Backtrack to the beginning to consume the characters raw
            _index = indexCheckpoint;
            return null;
        }

        if (!TryRead('"'))
            return null;

        var buffer = new StringBuilder();

        // Read characters until closing double quote while handling escapes
        while ((TryReadEscape() ?? TryRead(x => x != '"')) is { } c)
            buffer.Append(c);

        if (!TryRead('"'))
            return null;

        return new JsonString(buffer.ToString());
    }

    private JsonNode? TryReadArray()
    {
        if (!TryRead('['))
            return null;

        var children = new List<JsonNode>();
        do
        {
            SkipWhiteSpace();

            var child = TryReadNode();
            if (child is null)
                break;

            children.Add(child);

            SkipWhiteSpace();
        } while (TryRead(','));

        if (!TryRead(']'))
            return null;

        return new JsonArray(children.ToArray());
    }

    private JsonProperty? TryReadProperty()
    {
        var name = TryReadString()?.TryGetString();
        if (name is null)
            return null;

        SkipWhiteSpace();

        if (!TryRead(':'))
            return null;

        SkipWhiteSpace();

        var value = TryReadNode();
        if (value is null)
            return null;

        return new JsonProperty(name, value);
    }

    private JsonNode? TryReadObject()
    {
        if (!TryRead('{'))
            return null;

        var properties = new List<JsonProperty>();
        do
        {
            SkipWhiteSpace();

            var property = TryReadProperty();
            if (property is null)
                break;

            properties.Add(property);

            SkipWhiteSpace();
        } while (TryRead(','));

        if (!TryRead('}'))
            return null;

        return new JsonObject(properties.ToArray());
    }

    private JsonNode? TryReadNode() =>
        TryReadNull() ??
        TryReadBool() ??
        TryReadNumber() ??
        TryReadString() ??
        TryReadArray() ??
        TryReadObject();

    public JsonNode? TryReadDocument()
    {
        SkipWhiteSpace();

        var node = TryReadNode();
        if (node is null)
            return null;

        SkipWhiteSpace();

        // Ensure that the entire input has been consumed
        if (_index < _source.Length)
            return null;

        return node;
    }

    public JsonNode ReadDocument()
    {
        if (TryReadDocument() is { } result)
            return result;

        throw new InvalidOperationException(
            "Failed to parse JSON. " +
            $"Unexpected character sequence at position {_index}: '{_source.Substring(_index)}'."
        );
    }
}
#nullable restore