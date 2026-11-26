---
name: dotnet-csharp
description: Use ONLY when working on C#/.NET code, .csproj files, NuGet dependencies, build/test failures, or xUnit tests in this repository. Front-load C#, dotnet, csproj, NuGet, xUnit, async, nullable.
---

# .NET/C# Workflow

## Repo facts
- Library: `Ghis.PowershellLib` targeting `net10.0`
- Tests: `Ghis.PowershellLib.Tests` targeting `net10.0`, xUnit

## Workflow
1. Inspect relevant `.cs` and `.csproj` files before editing.
2. Keep public API behavior stable unless explicitly changing it.
3. Prefer small, idiomatic changes; match existing naming/style.
4. For dependency changes, check for vulnerabilities and use compatible secure versions.
5. Verify with:
   - `dotnet restore Ghis.PowershellLib.sln`
   - `dotnet build Ghis.PowershellLib.sln -c Release`
   - `dotnet test Ghis.PowershellLib.sln -c Release`

## C# guidelines
- Respect nullable/implicit usings defaults.
- Avoid unnecessary allocations and blocking calls in async flows.
- Add or update tests when behavior changes.
- Do not add comments unless requested.
