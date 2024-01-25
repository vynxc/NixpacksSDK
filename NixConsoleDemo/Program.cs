// See https://aka.ms/new-console-template for more information

using NixpacksSDK;
using NixpacksSDK.Build;

Console.WriteLine("Hello, World!");
var nix = new Nixpacks();
var build = nix.Build(new BuildOptions()
{
    Path = "/home/chayce/RiderProjects/NixpacksSDK/NixAspWebExample",
    Envs = new List<KeyValuePair<string, string>>()
    {
        new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Production")
    }
});
var result = await build.Result();
Console.WriteLine($"Is Broken Image: {result.IsBrokenImage}");
Console.WriteLine($"Build Error: {result.BuildError}");
Console.WriteLine($"Language: {result.Language}");
Console.WriteLine($"Install: {result.Install}");
Console.WriteLine($"Build: {result.Build}");
Console.WriteLine($"Start: {result.Start}");
Console.WriteLine($"Image: {result.ImageName}");