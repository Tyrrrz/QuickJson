using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace QuickJson;

internal abstract class JsonNode
{
    public virtual bool? TryGetBool() => null;

    public virtual double? TryGetNumber() => null;

    public virtual string? TryGetString() => null;

    public virtual IEnumerable<JsonNode> EnumerateChildren() => Enumerable.Empty<JsonNode>();

    public virtual IEnumerable<JsonProperty> EnumerateProperties() => Enumerable.Empty<JsonProperty>();

    public JsonNode? TryGetChild(int arrayIndex) =>
        EnumerateChildren().ElementAtOrDefault(arrayIndex);

    public JsonNode? TryGetChild(string propertyName) => EnumerateProperties()
        .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.Ordinal))?
        .Value;
}
#nullable restore