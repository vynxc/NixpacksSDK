using NixpacksSDK.Build;

namespace NixpacksSDK;

using System.Diagnostics;
using System.Threading.Tasks;

public class Nixpacks
{

    public BuildCmd Build(BuildOptions opt)
    {
        opt.Validate();

        ProcessStartInfo command;

        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
            case PlatformID.MacOSX:
                command = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $@"-c ""sudo nixpacks build '{opt.Path}' {opt.ToArgs()}""",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                break;
            case PlatformID.Win32NT:
                command = new ProcessStartInfo
                {
                    FileName = "nixpacks",
                    Arguments = $@"build ""{opt.Path}"" {opt.ToArgs()}",
                    RedirectStandardOutput = true,
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true,
                    Environment = { new KeyValuePair<string, string?>("NIXPACKS_CSHARP_SDK_VERSION","8") }
                };
                break;
            default:
                throw new NotSupportedException("This platform is not supported.");
        }

        Console.WriteLine(command.Arguments);
        return new BuildCmd(new Process { StartInfo = command });
    }

    public class BuildCmd(Process cmd)
    {
        private Process Cmd { get; } = cmd;

        public async Task<BuildOutput> Result()
        {
            var output = new BuildOutput();
            Cmd.Start();
            output.Response = await Cmd.StandardOutput.ReadToEndAsync();
            output.IsBrokenImage = Cmd.ExitCode != 0;
            output.Parse();

            return output;
        }
    }
}