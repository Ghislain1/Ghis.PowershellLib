# Ghis.Powershell.Lib

A lightweight C# wrapper around the PowerShell SDK for executing PowerShell commands from .NET applications.

[![Build Status](https://dev.azure.com/gzeleu/Ghis.PowershellLib/_apis/build/status/Ghislain1.Ghis.PowershellLib?branchName=master)](https://dev.azure.com/gzeleu/Ghis.PowershellLib/_build/latest?definitionId=1&branchName=master)

## Features

- Execute PowerShell scripts/commands from C#.
- Receive output lines through a callback.
- Run commands asynchronously with optional `CancellationToken`.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download) for building this repository.
- Windows PowerShell / PowerShell runtime compatible with the referenced PowerShell SDK.

## Installation

Install the NuGet package:

```bash
dotnet add package Ghis.Powershell.Lib
```

Or via the Package Manager Console:

```powershell
Install-Package Ghis.Powershell.Lib
```

## Usage

```csharp
using Ghis.PowershellLib;

IPowershellService powershellService = new PowershellService();

var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var results = new List<string>();

await powershellService.RunCommand(
    $"Get-ChildItem -Path \"{path}\" -File",
    filePath =>
    {
        Console.WriteLine(filePath);
        results.Add(filePath);
    });
```

With cancellation:

```csharp
using Ghis.PowershellLib;

IPowershellService powershellService = new PowershellService();
using var cts = new CancellationTokenSource();

IEnumerable<string> output = await powershellService.RunCommand(
    cts.Token,
    "Get-Command");
```

## Build and Test

From the repository root:

```bash
dotnet restore Ghis.PowershellLib.sln
dotnet build Ghis.PowershellLib.sln -c Release
dotnet test Ghis.PowershellLib.sln -c Release
```

## Getting Started with Coding Guidelines

- **Setup**: Install the .NET 10 SDK, clone the repository, and restore dependencies with `dotnet restore Ghis.PowershellLib.sln`.
- **Formatting**: Follow `.editorconfig` - CRLF line endings, 4 spaces for C#, 2 spaces for `.csproj` and JSON files.
- **Project defaults**: Keep `ImplicitUsings` and `Nullable` enabled; projects target `net10.0`.
- **Code style**: Match existing C# patterns, keep changes small, and preserve public API behavior unless intentionally changed and documented.
- **Tests**: Add or update xUnit tests for behavior changes.
- **Verification**: Before opening a PR, run:

  ```bash
  dotnet restore Ghis.PowershellLib.sln
  dotnet build Ghis.PowershellLib.sln -c Release
  dotnet test Ghis.PowershellLib.sln -c Release
  ```

- **Security and dependencies**: Never commit secrets or API keys; use compatible, secure NuGet versions and verify after updates.

For more details, see [CONTRIBUTING.md](CONTRIBUTING.md).

## Create a NuGet Package

The library project is configured to generate a NuGet package on build (`GeneratePackageOnBuild=true`). To create a package explicitly:

```bash
dotnet pack Ghis.Powershell.Lib/Ghis.Powershell.Lib.csproj -c Release -o artifacts
```

This creates a `.nupkg` file in the `artifacts` directory.

To publish the package to NuGet.org:

```bash
dotnet nuget push "artifacts/*.nupkg" --api-key <YOUR_NUGET_API_KEY> --source https://api.nuget.org/v3/index.json --skip-duplicate
```

For CI/CD, store the API key as a secret (for example, `NUGET_API_KEY` in GitHub Actions) and never commit it to the repository.

## CI/CD

- GitHub Actions CI: `.github/workflows/ci.yml` restores, builds, and tests on pushes and pull requests.
- GitHub Actions Release: `.github/workflows/release.yml` packs on `v*` tags, publishes to NuGet when `NUGET_API_KEY` is configured, and also runs every Monday at 06:00 UTC to create a weekly prerelease GitHub release with packaged artifacts.

## License

MIT. See [LICENSE](LICENSE).
