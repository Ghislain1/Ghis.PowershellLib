namespace Ghis.PowershellLib;

using System.Management.Automation;

public class PowershellService : IPowershellService
{
    public Task<IEnumerable<string>> RunCommand(CancellationToken cancelToken, string scriptString)
    {
        return this.RunCommand(scriptString, cancelToken);
    }

    public Task RunCommand(string scriptString, Action<string> onDataReceived)
    {
        return this.RunCommand(scriptString, onDataReceived, CancellationToken.None);
    }

    public async Task RunCommand(string scriptString, Action<string> onDataReceived, CancellationToken cancelToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scriptString);
        ArgumentNullException.ThrowIfNull(onDataReceived);

        var lines = await this.InvokeAsync(scriptString, cancelToken).ConfigureAwait(false);
        foreach (var line in lines)
        {
            onDataReceived(line);
        }
    }

    private async Task<IEnumerable<string>> RunCommand(string scriptString, CancellationToken cancelToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scriptString);
        return await this.InvokeAsync(scriptString, cancelToken).ConfigureAwait(false);
    }

    private async Task<List<string>> InvokeAsync(string scriptString, CancellationToken cancelToken)
    {
        using var ps = PowerShell.Create();
        ps.AddScript(scriptString);

        var output = new PSDataCollection<PSObject>();
        using var registration = cancelToken.Register(() =>
        {
            try
            {
                ps.Stop();
            }
            catch (ObjectDisposedException)
            {
            }
        });

        Task<PSDataCollection<PSObject>> invokeTask;
        try
        {
            invokeTask = ps.InvokeAsync<PSObject, PSObject>(null, output);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to start PowerShell command: {ex.Message}", ex);
        }

        try
        {
            await invokeTask.WaitAsync(cancelToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            throw new OperationCanceledException(cancelToken);
        }
        catch (PipelineStoppedException) when (cancelToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancelToken);
        }

        cancelToken.ThrowIfCancellationRequested();

        if (ps.HadErrors)
        {
            throw new InvalidOperationException(BuildErrorMessage(ps));
        }

        return output
            .Select(item => item?.BaseObject?.ToString() ?? item?.ToString())
            .Where(line => line is not null)
            .Cast<string>()
            .ToList();
    }

    private static string BuildErrorMessage(PowerShell ps)
    {
        var errors = ps.Streams.Error;
        if (errors.Count == 0)
        {
            return "PowerShell command failed.";
        }

        return string.Join(Environment.NewLine, errors.Select(e => e.ToString()));
    }
}