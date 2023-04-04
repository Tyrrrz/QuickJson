# QuickJson

[![Made in Ukraine](https://img.shields.io/badge/made_in-ukraine-ffd700.svg?labelColor=0057b7)](https://tyrrrz.me/ukraine)
[![Build](https://img.shields.io/github/actions/workflow/status/Tyrrrz/QuickJson/main.yml?branch=master)](https://github.com/Tyrrrz/QuickJson/actions)
[![Coverage](https://img.shields.io/codecov/c/github/Tyrrrz/QuickJson/master)](https://codecov.io/gh/Tyrrrz/QuickJson)
[![Version](https://img.shields.io/nuget/v/QuickJson.svg)](https://nuget.org/packages/QuickJson)
[![Downloads](https://img.shields.io/nuget/dt/QuickJson.svg)](https://nuget.org/packages/QuickJson)
[![Discord](https://img.shields.io/discord/869237470565392384?label=discord)](https://discord.gg/2SUWKFnHSm)
[![Donate](https://img.shields.io/badge/donate-$$$-8a2be2.svg)](https://tyrrrz.me/donate)
[![Fuck Russia](https://img.shields.io/badge/fuck-russia-e4181c.svg?labelColor=000000)](https://twitter.com/tyrrrz/status/1495972128977571848)

> 🟡 **Project status**: maintenance mode<sup>[[?]](https://github.com/Tyrrrz/.github/blob/master/docs/project-status.md)</sup>

**QuickJson** is a very basic JSON parser, distributed as a source-only package that can be referenced without imposing any run-time dependencies.

## Terms of use<sup>[[?]](https://github.com/Tyrrrz/.github/blob/master/docs/why-so-political.md)</sup>

By using this project or its source code, for any purpose and in any shape or form, you grant your **implicit agreement** to all the following statements:

- You **condemn Russia and its military aggression against Ukraine**
- You **recognize that Russia is an occupant that unlawfully invaded a sovereign state**
- You **support Ukraine's territorial integrity, including its claims over temporarily occupied territories of Crimea and Donbas**
- You **reject false narratives perpetuated by Russian state propaganda**

To learn more about the war and how you can help, [click here](https://tyrrrz.me/ukraine). Glory to Ukraine! 🇺🇦

## Install

- 📦 [NuGet](https://nuget.org/packages/QuickJson): `dotnet add package QuickJson`

> **Warning**:
> To use this package, your project needs to target C# 10 or later.
> You can ensure this by setting `<LangVersion>latest</LangVersion>` in the project file.

## Usage

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
