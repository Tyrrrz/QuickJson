﻿#nullable enable
namespace QuickJson;

// Partial class for extensibility
// ReSharper disable once PartialTypeWithSinglePart
internal partial class JsonProperty
{
    public string Name { get; }

    public JsonNode Value { get; }

    public JsonProperty(string name, JsonNode value)
    {
        Name = name;
        Value = value;
    }
}
