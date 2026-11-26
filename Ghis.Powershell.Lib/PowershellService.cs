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

        await foreach (var line in this.InvokeAsync(scriptString, cancelToken).ConfigureAwait(false))
        {
            onDataReceived(line);
        }
    }

    private async Task<IEnumerable<string>> RunCommand(string scriptString, CancellationToken cancelToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scriptString);

        var results = new List<string>();
        await foreach (var line in this.InvokeAsync(scriptString, cancelToken).ConfigureAwait(false))
        {
            results.Add(line);
        }

        return results;
    }

    private async IAsyncEnumerable<string> InvokeAsync(string scriptString, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancelToken)
    {
        using var ps = PowerShell.Create();
        ps.AddScript(scriptString);

        var output = new PSDataCollection<PSObject>();
        using var registration = cancelToken.Register(() => ps.Stop());

        var invokeTask = ps.InvokeAsync<PSObject, PSObject>(null, output, null, null, null);

        await foreach (var item in output.ReadAllAsync(cancelToken).ConfigureAwait(false))
        {
            cancelToken.ThrowIfCancellationRequested();
            var line = item?.BaseObject?.ToString() ?? item?.ToString();
            if (line is not null)
            {
                yield return line;
            }
        }

        try
        {
            await invokeTask.ConfigureAwait(false);
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