# Contributing

Thank you for contributing to `Ghis.Powershell.Lib`.

## Development Setup

- Install the [.NET 10 SDK](https://dotnet.microsoft.com/download).
- Clone the repository and restore dependencies:

```bash
dotnet restore Ghis.PowershellLib.sln
```

## Build and Test

Run the standard verification commands from the repository root:

```bash
dotnet restore Ghis.PowershellLib.sln
dotnet build Ghis.PowershellLib.sln -c Release
dotnet test Ghis.PowershellLib.sln -c Release
```

## Coding Guidelines

- **Project defaults**: Keep `ImplicitUsings` and `Nullable` enabled; projects target `net10.0`.
- **Style**: Match existing C# naming and patterns. Prefer small, focused changes.
- **Public API**: Preserve public API behavior unless the change is intentional and clearly documented.
- **Tests**: Add or update xUnit tests when behavior changes.
- **Dependencies**: Use compatible, secure NuGet package versions. After dependency changes, verify restore/build/test and address vulnerability warnings when possible.
- **Security**: Never commit secrets, API keys, or credentials.

## Code Formatting

This repository uses `.editorconfig` as the source of truth for formatting.

- **Line endings**: `end_of_line = CRLF` for all files.
- **C# files (`*.cs`)**: `indent_style = space`, `indent_size = 4`.
- **Project files (`*.csproj`)**: `indent_style = space`, `indent_size = 2`.
- **JSON files (`*.json`)**: `indent_style = space`, `indent_size = 2`.
- **Namespaces**: use file-scoped namespaces (`namespace Ghis.PowershellLib;`).
- **Using directives**: place `using` directives after the file-scoped namespace declaration.

Before submitting changes, format the code with:

```bash
dotnet format Ghis.PowershellLib.sln
```

To verify formatting without changing files (useful in CI):

```bash
dotnet format Ghis.PowershellLib.sln --verify-no-changes
```

## Packaging

The library project generates a NuGet package on build. To pack explicitly:

```bash
dotnet pack Ghis.Powershell.Lib/Ghis.Powershell.Lib.csproj -c Release -o artifacts
```

## Pull Requests

- Describe the problem, the approach, and how you verified the change.
- Keep pull requests focused and include relevant tests.
- Ensure CI passes before requesting review.
