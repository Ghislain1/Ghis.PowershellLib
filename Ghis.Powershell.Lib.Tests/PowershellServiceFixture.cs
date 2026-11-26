namespace Ghis.Powershell.Lib.Tests;

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
        Assert.NotEmpty(output);
        Assert.Contains("hello", output);
    }

    [Fact]
    public async Task RunCommand_WithCallback_ReceivesOutput()
    {
        IPowershellService powershellService = new PowershellService();
        var output = new List<string>();

        await powershellService.RunCommand("Write-Output 'hello'", output.Add);

        Assert.Contains("hello", output);
    }

    [Fact]
    public async Task RunCommand_WithCallbackAndCancellationToken_ReceivesOutput()
    {
        IPowershellService powershellService = new PowershellService();
        var output = new List<string>();
        using var cts = new CancellationTokenSource();

        await powershellService.RunCommand("Write-Output 'hello'", output.Add, cts.Token);

        Assert.Contains("hello", output);
    }

    [Fact]
    public async Task RunCommand_WithScript_ReturnsAllLines()
    {
        IPowershellService powershellService = new PowershellService();

        var result = await powershellService.RunCommand(CancellationToken.None, "Write-Output 'alpha'; Write-Output 'beta'");

        var output = result.ToList();
        Assert.Equal(["alpha", "beta"], output);
    }
}