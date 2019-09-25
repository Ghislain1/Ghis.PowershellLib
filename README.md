# PowershellService Library
A simple  warpper library for Powershell in C#

##  Status badge
[![Build Status](https://dev.azure.com/gzeleu/Ghis.PowershellLib/_apis/build/status/Ghislain1.Ghis.PowershellLib?branchName=master)](https://dev.azure.com/gzeleu/Ghis.PowershellLib/_build/latest?definitionId=1&branchName=master)

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

## Usage

```c#
       IPowershellService powershellService = new PowershellService();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);

            await powershellService.RunCommand($"Get-ChildItem -Path {path} -Recurse -Force -File", filePath =>
             {
                 Console.WriteLine($" -->  {filePath}");
                 result.Add(filePath);
             });
```
