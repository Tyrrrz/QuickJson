# QuickJson

[![Status](https://img.shields.io/badge/status-maintenance-ffd700.svg)](https://github.com/Tyrrrz/.github/blob/prime/docs/project-status.md)
[![Build](https://img.shields.io/github/actions/workflow/status/Tyrrrz/QuickJson/main.yml?branch=prime)](https://github.com/Tyrrrz/QuickJson/actions)
[![Coverage](https://img.shields.io/codecov/c/github/Tyrrrz/QuickJson/prime)](https://codecov.io/gh/Tyrrrz/QuickJson)
[![Version](https://img.shields.io/nuget/v/QuickJson.svg)](https://nuget.org/packages/QuickJson)
[![Downloads](https://img.shields.io/nuget/dt/QuickJson.svg)](https://nuget.org/packages/QuickJson)
[![Discord](https://img.shields.io/discord/869237470565392384?label=discord)](https://discord.gg/2SUWKFnHSm)

<table>
    <tr>
        <td width="99999" align="center">Development of this project is entirely funded by the community. <b><a href="https://tyrrrz.me/donate">Consider donating to support!</a></b></td>
    </tr>
</table>

<p align="center">
    <img src="favicon.png" alt="Icon" />
</p>

> **Made in 🇺🇦 Ukraine — [despite Russia's best efforts](https://tyrrrz.me/ukraine)**.
> Using this project or its source code comes with the implicit agreement that you condemn Russia's war of aggression against Ukraine, recognize Russia as a terrorist state, and support Ukraine's territorial integrity.

**QuickJson** is a very basic, lightweight JSON parser.

## Install

- 📦 [NuGet](https://nuget.org/packages/QuickJson): `dotnet add package QuickJson`

> [!TIP]
> You can use [**Binternal**](https://github.com/Tyrrrz/Binternal) to internalize this library if you prefer to avoid taking an external dependency.

## Usage

To parse a JSON string, call `Json.TryParse(...)` or `Json.Parse(...)`:

```csharp
// This returns null on invalid JSON
var json = Json.TryParse(
    """
    {
        "foo": [
            69,
            true,
            "bar"
        ]
    }
    """
);

// This throws on invalid JSON
var json = Json.Parse("...");
```

To retrieve a nested node in the parsed JSON, use `[Try]GetChild(...)`:

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

In order to extract values from nodes, use `[Try]GetNumber()`, `[Try]GetBool()`, or `[Try]GetString()`:

```csharp
// May return null if the node contains a value of a different kind
var value1 = child1?.TryGetNumber(); // double
var value2 = child2?.TryGetBool(); // bool
var value3 = child3?.TryGetString(); // string
```
