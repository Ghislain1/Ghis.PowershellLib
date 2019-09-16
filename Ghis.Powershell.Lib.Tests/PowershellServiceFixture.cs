using System.Threading;
using System.Threading.Tasks;
using Ghis.PowershellLib;
using Xunit;

namespace Ghis.Powershell.Lib.Tests
{
    public class PowershellServiceFixture
    {
        [Fact]
        public async Task ShouldNotFailed()
        {
            IPowershellService powershellService = new PowershellService();
            var list = await powershellService.RunCommand(CancellationToken.None, scriptString: "Get-Command ");
            var expected = 0;
            foreach (var item in list)
            {
                expected++;
            }
            Assert.True(expected > 0, "list is not empty");
        }
    }
}