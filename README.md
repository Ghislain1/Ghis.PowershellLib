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

For GitHub Actions, use [NuGet Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing).

### Getting Started with NuGet Trusted Publishing

1. Sign in to [NuGet.org](https://www.nuget.org/).
2. Open your username menu and choose **Trusted Publishing**.
3. Add a new policy:
   - **Package owner**: your user or organization.
   - **Repository Owner**: `Ghislain1`
   - **Repository**: `Ghis.PowershellLib`
   - **Workflow File**: `release.yml` (file name only)
   - **Environment**: `production` (must match the workflow job environment).
4. In GitHub repository settings, add a secret named `NUGET_USER` with your NuGet.org profile name (not email).
5. Push a `v*` tag to publish. The release workflow uses `NuGet/login@v1` with OIDC to obtain a short-lived NuGet API key and publish the package.

## CI/CD

- GitHub Actions CI: `.github/workflows/ci.yml` restores, builds, and tests on pushes and pull requests.
- GitHub Actions Release: `.github/workflows/release.yml` creates a prerelease GitHub release on each push to `master`/`main` and every 3 weeks on Monday at 06:00 UTC (scheduled runs). Pushing a `v*` tag creates/updates a GitHub release and publishes the package to NuGet via Trusted Publishing (`NUGET_USER`).

## License

MIT. See [LICENSE](LICENSE).
