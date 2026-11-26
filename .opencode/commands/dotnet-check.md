---
description: Restore, build, and test the .NET solution.
agent: build
---

Run the standard verification for this repository.

1. Execute `dotnet restore Ghis.PowershellLib.sln`
2. Execute `dotnet build Ghis.PowershellLib.sln -c Release`
3. Execute `dotnet test Ghis.PowershellLib.sln -c Release`
4. Report failures with file/line references and the next fix.
