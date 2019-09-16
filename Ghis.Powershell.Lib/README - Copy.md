# PowershellService Library
A simple package with guard clause extensions.

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