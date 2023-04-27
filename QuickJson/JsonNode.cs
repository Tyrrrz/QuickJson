using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace QuickJson;

internal abstract class JsonNode
{
    /// <summary>
    /// Attempts to extract a boolean value from this JSON node.
    /// Returns null if the node does not contain a boolean value.
    /// </summary>
    public virtual bool? TryGetBool() => null;

    /// <summary>
    /// Extracts a boolean value from this JSON node.
    /// </summary>
    public bool GetBool() =>
        TryGetBool() ??
        throw new InvalidOperationException(
            $"Failed to extract a boolean value from a JSON node of type '{GetType().Name}'."
        );

    /// <summary>
    /// Attempts to extract a number value from this JSON node.
    /// Returns null if the node does not contain a number value.
    /// </summary>
    public virtual double? TryGetNumber() => null;

    /// <summary>
    /// Extracts a number value from this JSON node.
    /// </summary>
    public double GetNumber() =>
        TryGetNumber() ??
        throw new InvalidOperationException(
            $"Failed to extract a number value from a JSON node of type '{GetType().Name}'."
        );

    /// <summary>
    /// Attempts to extract a string value from this JSON node.
    /// Returns null if the node does not contain a string value.
    /// </summary>
    public virtual string? TryGetString() => null;

    /// <summary>
    /// Extracts a string value from this JSON node.
    /// </summary>
    public string GetString() =>
        TryGetString() ??
        throw new InvalidOperationException(
            $"Failed to extract a string value from a JSON node of type '{GetType().Name}'."
        );

    /// <summary>
    /// Enumerates all children of this JSON node.
    /// Returns an empty sequence if the node is not an array.
    /// </summary>
    public virtual IEnumerable<JsonNode> EnumerateChildren() => Enumerable.Empty<JsonNode>();

    /// <summary>
    /// Enumerates all properties of this JSON node.
    /// Returns an empty sequence if the node is not an object.
    /// </summary>
    public virtual IEnumerable<JsonProperty> EnumerateProperties() => Enumerable.Empty<JsonProperty>();

    /// <summary>
    /// Attempts to extract a child node at the specified index from this JSON node.
    /// Returns null if the node is not an array or if the index is out of range.
    /// </summary>
    public JsonNode? TryGetChild(int arrayIndex) =>
        EnumerateChildren().ElementAtOrDefault(arrayIndex);

    /// <summary>
    /// Extracts a child node at the specified index from this JSON node.
    /// </summary>
    public JsonNode GetChild(int arrayIndex) =>
        TryGetChild(arrayIndex) ??
        throw new InvalidOperationException(
            $"Failed to extract a child node at index {arrayIndex} from a JSON node of type '{GetType().Name}'."
        );

    /// <summary>
    /// Attempts to extract a child node with the specified name from this JSON node.
    /// Returns null if the node is not an object or if the name is not found.
    /// </summary>
    public JsonNode? TryGetChild(string propertyName) => EnumerateProperties()
        .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.Ordinal))?
        .Value;

    /// <summary>
    /// Extracts a child node with the specified name from this JSON node.
    /// </summary>
    public JsonNode GetChild(string propertyName) =>
        TryGetChild(propertyName) ??
        throw new InvalidOperationException(
            $"Failed to extract a child node with name '{propertyName}' from a JSON node of type '{GetType().Name}'."
        );
}