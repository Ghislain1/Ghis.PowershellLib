namespace Ghis.Powershell.Lib.Tests;

using FluentAssertions;
using Ghis.PowershellLib;
using Xunit;

public class PowershellServiceFixture
{
    [Fact]
    public async Task RunCommand_WithScript_ReturnsOutput()
    {
        IPowershellService powershellService = new PowershellService();

        var result = await powershellService.RunCommand(CancellationToken.None, "Write-Output 'hello'");

        var output = result.ToList();
        output.Should().NotBeEmpty();
        output.Should().Contain("hello");
    }

    [Fact]
    public async Task RunCommand_WithCallback_ReceivesOutput()
    {
        IPowershellService powershellService = new PowershellService();
        var output = new List<string>();

        await powershellService.RunCommand("Write-Output 'hello'", output.Add);

        output.Should().Contain("hello");
    }

    [Fact]
    public async Task RunCommand_WithCallbackAndCancellationToken_ReceivesOutput()
    {
        IPowershellService powershellService = new PowershellService();
        var output = new List<string>();
        using var cts = new CancellationTokenSource();

        await powershellService.RunCommand("Write-Output 'hello'", output.Add, cts.Token);

        output.Should().Contain("hello");
    }

    [Fact]
    public async Task RunCommand_WithScript_ReturnsAllLines()
    {
        IPowershellService powershellService = new PowershellService();

        var result = await powershellService.RunCommand(CancellationToken.None, "Write-Output 'alpha'; Write-Output 'beta'");

        var output = result.ToList();
        output.Should().Equal("alpha", "beta");
    }
}