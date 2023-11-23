# Changelog

> **Important**:
> This changelog is no longer maintained and will be removed in the future.
> Going forward, new versions of this package will have the corresponding release notes published on [GitHub Releases](https://github.com/Tyrrrz/QuickJson/releases).

## v1.1 (27-Apr-2023)

- Added non-try method alternatives for `TryGetBool()`, `TryGetNumber()`, `TryGetString()`, `TryGetChild(...)`. Calling the non-try variant will throw an exception in case of failure instead of returning `null`.

## v1.0.2 (07-Apr-2023)

- Added support for parsing number nodes that use exponential notation.

## v1.0.1 (06-Apr-2023)

- Fixed an issue where negative numbers were not parsed correctly.
