# AGENTS.md

## Project
- Library: `Ghis.Powershell.Lib` (net10.0) wrapping PowerShell SDK usage.
- Tests: `Ghis.Powershell.Lib.Tests` (xUnit, net10.0).

## Build & Test
- Restore: `dotnet restore Ghis.PowershellLib.sln`
- Build: `dotnet build Ghis.PowershellLib.sln -c Release`
- Test: `dotnet test Ghis.PowershellLib.sln -c Release`

## Conventions
- Follow existing C# style in touched files; do not add comments unless requested.
- Prefer small, targeted changes; preserve public API behavior unless requested.
- Keep nullable and implicit-usings enabled project defaults.
- When changing dependencies, verify restore/build/test and address vulnerability warnings when possible.
- Do not commit unless explicitly asked.
