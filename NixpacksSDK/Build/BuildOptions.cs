namespace NixpacksSDK.Build;

using System;
using System.Collections.Generic;

public class BuildOptions
{
    public string Path { get; set; }
    public string Name { get; set; }
    public string Output { get; set; }
    public string JsonPlan { get; set; }
    public string Tag { get; set; }
    public string InstallCommand { get; set; }
    public List<KeyValuePair<string,string>> Labels { get; set; }
    public string BuildCommand { get; set; }
    public string Platform { get; set; }
    public string StartCommand { get; set; }
    public bool CurrentDirectory { get; set; }
    public List<string> NixPackages { get; set; }
    public List<string> AptPackages { get; set; }
    public bool NoCache { get; set; }
    public List<string> NixLibraries { get; set; }
    public List<KeyValuePair<string,string>> Envs { get; set; }
    public string Config { get; set; }
    public bool NoErrorWithoutStartCommand { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Path))
        {
            throw new ArgumentException("Path must be specified");
        }
    }

    public string ToArgs()
    {
        var args = new List<string>();

        if (!string.IsNullOrEmpty(Name))
        {
            args.Add("--name");
            args.Add(Name);
        }

        if (!string.IsNullOrEmpty(Output))
        {
            args.Add("--out");
            args.Add(Output);
        }

        if (!string.IsNullOrEmpty(JsonPlan))
        {
            args.Add("--json-plan");
            args.Add(JsonPlan);
        }

        if (!string.IsNullOrEmpty(Tag))
        {
            args.Add("--tag");
            args.Add(Tag);
        }

        if (!string.IsNullOrEmpty(InstallCommand))
        {
            args.Add("--install-cmd");
            args.Add(InstallCommand);
        }

        if (Labels != null)
        {
            foreach (var label in Labels)
            {
                args.Add("--label");
                args.Add($"{label.Key}={label.Value}");
            }
        }

        if (!string.IsNullOrEmpty(BuildCommand))
        {
            args.Add("--build-cmd");
            args.Add(BuildCommand);
        }

        if (!string.IsNullOrEmpty(Platform))
        {
            args.Add("--platform");
            args.Add(Platform);
        }

        if (!string.IsNullOrEmpty(StartCommand))
        {
            args.Add("--start-cmd");
            args.Add(StartCommand);
        }

        if (CurrentDirectory)
        {
            args.Add("--current-directory");
        }

        if (NixPackages != null)
        {
            foreach (var pkg in NixPackages)
            {
                args.Add("--pkgs");
                args.Add(pkg);
            }
        }

        if (AptPackages != null)
        {
            foreach (var pkg in AptPackages)
            {
                args.Add("--apt");
                args.Add(pkg);
            }
        }

        if (NoCache)
        {
            args.Add("--no-cache");
        }

        if (NixLibraries != null)
        {
            foreach (var lib in NixLibraries)
            {
                args.Add("--libs");
                args.Add(lib);
            }
        }

        if (Envs != null)
        {
            foreach (var env in Envs)
            {
                args.Add("--env");
                args.Add($"{env.Key}={env.Value}");
            }
        }

        if (!string.IsNullOrEmpty(Config))
        {
            args.Add("--config");
            args.Add(Config);
        }

        if (NoErrorWithoutStartCommand)
        {
            args.Add("--no-error-without-start-cmd");
        }

        args.Add("--verbose");

        return string.Join(' ',args);
    }
}

