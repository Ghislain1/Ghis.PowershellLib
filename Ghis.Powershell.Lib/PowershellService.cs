using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;

namespace Ghis.PowershellLib
{
    public class PowershellService : IPowershellService
    {
        public Task<IEnumerable<string>> RunCommand(CancellationToken cancelToken, string scriptString)
        {
            var tcs = new TaskCompletionSource<IEnumerable<string>>();

            Task.Factory.StartNew(() =>
            {
                using (var ps = PowerShell.Create())
                {
                    var collection = ps.AddScript(scriptString).Invoke<string>();

                    tcs.TrySetResult(collection);
                }
            });

            return tcs.Task;
        }

        public Task RunCommand(string scriptString, Action<string> onDataReceived)
        {
            return this.RunCommand(scriptString, onDataReceived, CancellationToken.None);
        }

        public Task RunCommand(string scriptString, Action<string> onDataReceived, CancellationToken cancelToken)
        {
            var tcs = new TaskCompletionSource<object>();

            Task.Run(() =>
            {
                using (var ps = PowerShell.Create())
                {
                    var collection = ps.AddScript(scriptString).Invoke<string>();
                    foreach (var line in collection)
                    {
                        onDataReceived(line);
                    }

                    tcs.TrySetResult(null);
                }
            }, cancelToken);

            return tcs.Task;
        }
    }
}