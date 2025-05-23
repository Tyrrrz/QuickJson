﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonReader(string source)
{
    private int _position;

    private char? TryRead(Func<char, bool> predicate)
    {
        if (_position >= source.Length)
            return null;

        var ch = source[_position];
        if (!predicate(ch))
            return null;

        _position++;
        return ch;
    }

    private bool TryRead(char expectedChar) => TryRead(c => c == expectedChar) is not null;

    private string? TryRead(int length, Func<string, bool> predicate)
    {
        if (_position + length > source.Length)
            return null;

        var str = source.Substring(_position, length);
        if (!predicate(str))
            return null;

        _position += length;
        return str;
    }

    private bool TryRead(string expectedString) =>
        TryRead(
            expectedString.Length,
            s => string.Equals(s, expectedString, StringComparison.Ordinal)
        )
            is not null;

    private void SkipWhiteSpace()
    {
        while (TryRead(char.IsWhiteSpace) is not null)
        {
            // Nom nom
        }
    }

    private JsonNode? TryReadNull() => TryRead("null") ? JsonNull.Instance : null;

    private JsonNode? TryReadBool()
    {
        JsonNode? TryReadBoolTrue() => TryRead("true") ? JsonBool.True : null;

        JsonNode? TryReadBoolFalse() => TryRead("false") ? JsonBool.False : null;

        return TryReadBoolTrue() ?? TryReadBoolFalse();
    }

    private JsonNode? TryReadNumber()
    {
        var buffer = new StringBuilder();

        // Sign
        if (TryRead('-'))
            buffer.Append('-');

        // Digits
        while (TryRead(char.IsDigit) is { } digit)
            buffer.Append(digit);

        // Decimal point and the following digits
        if (TryRead('.'))
        {
            buffer.Append('.');
            while (TryRead(char.IsDigit) is { } digit)
                buffer.Append(digit);

            // Exponent and the following digits
            if (TryRead('e') || TryRead('E'))
            {
                buffer.Append('e');
                if (TryRead(c => c is '+' or '-') is { } sign)
                    buffer.Append(sign);

                while (TryRead(char.IsDigit) is { } digit)
                    buffer.Append(digit);
            }
        }

        return double.TryParse(
            buffer.ToString(),
            NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint
                | NumberStyles.AllowExponent,
            CultureInfo.InvariantCulture,
            out var value
        )
            ? new JsonNumber(value)
            : null;
    }

    private JsonNode? TryReadString()
    {
        char? TryReadEscape()
        {
            var checkpoint = _position;

            if (!TryRead('\\'))
                return null;

            if (
                TryRead(c => c is '\\' or '"' or '/' or 'b' or 'f' or 'n' or 'r' or 't' or 'u') is
                { } escaped
            )
            {
                // Unicode character escape, read the following 4 hex digits as the codepoint
                if (escaped == 'u')
                {
                    var hexSequence = TryRead(4, s => Regex.IsMatch(s, @"^[0-9a-fA-F]{4}$"));
                    if (
                        hexSequence is not null
                        && short.TryParse(
                            hexSequence,
                            NumberStyles.AllowHexSpecifier,
                            CultureInfo.InvariantCulture,
                            out var codepoint
                        )
                    )
                    {
                        return (char)codepoint;
                    }

                    // Backtrack to the beginning to consume the characters raw
                    _position = checkpoint;
                    return null;
                }

                // Basic escape
                return escaped switch
                {
                    'b' => '\b',
                    'f' => '\f',
                    'n' => '\n',
                    'r' => '\r',
                    't' => '\t',
                    _ => escaped,
                };
            }

            // Backtrack to the beginning to consume the characters raw
            _position = checkpoint;
            return null;
        }

        if (!TryRead('"'))
            return null;

        var buffer = new StringBuilder();

        // Read characters until the closing double quote while handling escapes
        while ((TryReadEscape() ?? TryRead(c => c != '"')) is { } ch)
            buffer.Append(ch);

        if (!TryRead('"'))
            return null;

        return new JsonString(buffer.ToString());
    }

    private JsonNode? TryReadArray()
    {
        IEnumerable<JsonNode> ReadArrayChildren()
        {
            do
            {
                SkipWhiteSpace();

                var child = TryReadNode();
                if (child is null)
                    break;

                yield return child;

                SkipWhiteSpace();
            } while (TryRead(','));
        }

        if (!TryRead('['))
            return null;

        var children = ReadArrayChildren().ToArray();

        if (!TryRead(']'))
            return null;

        return new JsonArray(children);
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
        IEnumerable<JsonProperty> ReadObjectProperties()
        {
            do
            {
                SkipWhiteSpace();

                var property = TryReadProperty();
                if (property is null)
                    break;

                yield return property;

                SkipWhiteSpace();
            } while (TryRead(','));
        }

        if (!TryRead('{'))
            return null;

        var properties = ReadObjectProperties().ToArray();

        if (!TryRead('}'))
            return null;

        return new JsonObject(properties);
    }

    private JsonNode? TryReadNode() =>
        TryReadNull()
        ?? TryReadBool()
        ?? TryReadNumber()
        ?? TryReadString()
        ?? TryReadArray()
        ?? TryReadObject();

    public JsonNode? TryReadDocument()
    {
        SkipWhiteSpace();

        var node = TryReadNode();
        if (node is null)
            return null;

        SkipWhiteSpace();

        // Ensure that the entire input has been consumed
        if (_position < source.Length)
            return null;

        return node;
    }

    public JsonNode ReadDocument()
    {
        if (TryReadDocument() is { } result)
            return result;

        var remainingSource = source.Substring(
            _position,
            // Limit the reported remainder to a reasonable length
            Math.Min(source.Length - _position, 100)
        );

        throw new InvalidOperationException(
            "Failed to parse JSON. "
                + $"Unexpected character sequence at position {_position}: '{remainingSource}'."
        );
    }
}
