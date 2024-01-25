namespace NixpacksSDK.Plan;

public class PlanOptions
{
    /// <summary>
    /// Specifies the path
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// Specifies the command to install language dependencies
    /// </summary>
    public string InstallCommand { get; set; }
    
    /// <summary>
    /// Specifies the command to build the image, it will overwrite the default build command
    /// </summary>
    public string BuildCommand { get; set; }
    
    /// <summary>
    /// Specifies the command to run when starting the container
    /// </summary>
    public string StartCommand { get; set; }
    
    /// <summary>
    /// Specifies additional nix packages to install in the environment
    /// </summary>
    public List<string> NixPackages { get; set; }
    
    /// <summary>
    /// Specifies additional apt packages to install in the environment
    /// </summary>
    public List<string> AptPackages { get; set; }
    
    /// <summary>
    /// Specifies additional nix libraries to install in the environment
    /// </summary>
    public List<string> NixLibraries { get; set; }

    // Presuming the Env class looks something like this -
    /*public class Env
{
    public string Key {get; set;}
    public string Value {get; set;}
}*/
    public List<KeyValuePair<string,string>> Envs { get; set; }
    
    /// <summary>
    /// Specifies path to config file
    /// </summary>
    public string Config { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Path))
        {
            throw new ArgumentException("Path is required");
        }
    }

    public List<string> ToArgs()
    {
        var args = new List<string>();

        if (!string.IsNullOrEmpty(InstallCommand))
        {
            args.Add("--install-cmd");
            args.Add(InstallCommand);
        }

        if (!string.IsNullOrEmpty(BuildCommand))
        {
            args.Add("--build-cmd");
            args.Add(BuildCommand);
        }

        if (!string.IsNullOrEmpty(StartCommand))
        {
            args.Add("--start-cmd");
            args.Add(StartCommand);
        }

        if (NixPackages != null && NixPackages.Count != 0)
        {
            foreach (var pkg in NixPackages)
            {
                args.Add("--pkgs");
                args.Add(pkg);
            }
        }

        if (AptPackages != null && AptPackages.Count != 0)
        {
            foreach (var pkg in AptPackages)
            {
                args.Add("--apt");
                args.Add(pkg);
            }
        }

        if (NixLibraries != null && NixLibraries.Count != 0)
        {
            foreach (var lib in NixLibraries)
            {
                args.Add("--libs");
                args.Add(lib);
            }
        }

        if (Envs != null && Envs.Count != 0)
        {
            foreach (var env in Envs)
            {
                args.Add("--env");
                args.Add(env.Key + "=" + env.Value);
            }
        }

        if (!string.IsNullOrEmpty(Config))
        {
            args.Add("--config");
            args.Add(Config);
        }

        return args;
    }
}