# QuickJson

[![Build](https://github.com/Tyrrrz/QuickJson/workflows/CI/badge.svg?branch=master)](https://github.com/Tyrrrz/QuickJson/actions)
[![Coverage](https://codecov.io/gh/Tyrrrz/QuickJson/branch/master/graph/badge.svg)](https://codecov.io/gh/Tyrrrz/QuickJson)
[![Version](https://img.shields.io/nuget/v/QuickJson.svg)](https://nuget.org/packages/QuickJson)
[![Downloads](https://img.shields.io/nuget/dt/QuickJson.svg)](https://nuget.org/packages/QuickJson)
[![Discord](https://img.shields.io/discord/869237470565392384?label=discord)](https://discord.gg/2SUWKFnHSm)
[![Donate](https://img.shields.io/badge/donate-$$$-purple.svg)](https://tyrrrz.me/donate)

✅ **Project status: active**. [What does it mean?](https://github.com/Tyrrrz/shared/blob/master/docs/project-status.md)

**QuickJson** is a very basic JSON parser distributed as a source-only NuGet package, which allows it to be referenced without imposing any runtime dependencies.

## Download

📦 [NuGet](https://nuget.org/packages/QuickJson): `dotnet add package QuickJson`

## Usage

> ⚠ To use this package, your project needs to target C# 8 or later.
You can ensure this by setting `<LangVersion>latest</LangVersion>` in the project file.

To parse a JSON string, call `Json.TryParse(...)` or `Json.Parse(...)`:

```csharp
// This returns null on invalid JSON
var json = Json.TryParse("{ \"foo\": [ 69, true, \"bar\" ] }");

// This throws on invalid JSON
var json = Json.Parse("{ \"foo\": [ 69, true, \"bar\" ] }");
```

To retrieve a nested node in the parsed JSON, use `TryGetChild(...)`:

```csharp
// May return null if the property doesn't exist
// or if the referenced node is not an object
var foo = json.TryGetChild("foo");

// May return null if the child doesn't exist
// or if the referenced node is not an array
var child1 = foo?.TryGetChild(0);
var child2 = foo?.TryGetChild(1);
var child3 = foo?.TryGetChild(2);
```

Alternatively, you can also enumerate all object properties using `EnumerateProperties()` or all array children using `EnumerateChildren()`:

```csharp
// Returns an empty enumerator if the referenced node is not an object
foreach (var prop in json.EnumerateProperties())
{
    var name = prop.Name; // string
    var value = prop.Value; // JsonNode
    
    // ...
}

// Returns an empty enumerator if the referenced node is not an array
foreach (var child in json.EnumerateChildren())
{
    // ...
}
```

In order to extract values from nodes, use `TryGetNumber()`, `TryGetBool()`, or `TryGetString()`:

```csharp
// May return null if the node contains a value of a different kind
var value1 = child1?.TryGetNumber(); // 69
var value2 = child2?.TryGetBool(); // true
var value3 = child3?.TryGetString(); // "bar"
```