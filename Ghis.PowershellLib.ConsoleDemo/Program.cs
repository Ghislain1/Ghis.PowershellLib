using System;
using System.Threading.Tasks;

namespace Ghis.PowershellLib.ConsoleDemo
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IPowershellService powershellService = new PowershellService();

            var scruipt = $"Get-ChildItem  -Path ./ -Recurse -File -Force";
            await powershellService.RunCommand(scruipt, filePath =>
              {
                  Console.WriteLine(filePath);
              });

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}