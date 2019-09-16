using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ghis.PowershellLib
{
    /// <summary>
    /// </summary>
    public interface IPowershellService
    {
        Task<IEnumerable<string>> RunCommand(CancellationToken cancelToken, string scriptString);

        /// <summary>
        /// Runs the command. How to use it: RunCommand($"Get-ChildItem -Path {path} -Recurse -Force
        /// -File", filePath =&gt; Console.WriteLine($" --&gt; {filePath}");
        /// </summary>
        /// <param name="scriptString">The script string.</param>
        /// <param name="onDataReceived">The on data received.</param>
        /// <returns></returns>
        Task RunCommand(string scriptString, Action<string> onDataReceived);

        Task RunCommand(string scriptString, Action<string> onDataReceived, CancellationToken cancelToken);
    }
}