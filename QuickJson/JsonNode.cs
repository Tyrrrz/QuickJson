using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace QuickJson;

internal abstract class JsonNode
{
    public virtual bool? TryGetBool() => null;

    public bool GetBool() =>
        TryGetBool() ??
        throw new InvalidOperationException(
            $"Failed to extract a boolean value from a JSON node of type '{GetType().Name}'."
        );

    public virtual double? TryGetNumber() => null;

    public double GetNumber() =>
        TryGetNumber() ??
        throw new InvalidOperationException(
            $"Failed to extract a number value from a JSON node of type '{GetType().Name}'."
        );

    public virtual string? TryGetString() => null;

    public string GetString() =>
        TryGetString() ??
        throw new InvalidOperationException(
            $"Failed to extract a string value from a JSON node of type '{GetType().Name}'."
        );

    public virtual IEnumerable<JsonNode> EnumerateChildren() => Enumerable.Empty<JsonNode>();

    public virtual IEnumerable<JsonProperty> EnumerateProperties() => Enumerable.Empty<JsonProperty>();

    public JsonNode? TryGetChild(int arrayIndex) =>
        EnumerateChildren().ElementAtOrDefault(arrayIndex);

    public JsonNode GetChild(int arrayIndex) =>
        TryGetChild(arrayIndex) ??
        throw new InvalidOperationException(
            $"Failed to extract a child node at index {arrayIndex} from a JSON node of type '{GetType().Name}'."
        );

    public JsonNode? TryGetChild(string propertyName) => EnumerateProperties()
        .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.Ordinal))?
        .Value;

    public JsonNode GetChild(string propertyName) =>
        TryGetChild(propertyName) ??
        throw new InvalidOperationException(
            $"Failed to extract a child node with name '{propertyName}' from a JSON node of type '{GetType().Name}'."
        );
}