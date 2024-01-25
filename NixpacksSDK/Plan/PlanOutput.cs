namespace NixpacksSDK.Plan;

using Newtonsoft.Json;
using System.Collections.Generic;

public class PlanOutput
{
    public byte[] Response { get; set; }

    [JsonProperty("providers")]
    public List<string> Providers { get; set; }

    [JsonProperty("buildImage")]
    public string BuildImage { get; set; }

    [JsonProperty("variables")]
    public Dictionary<string, string> Variables { get; set; }

    [JsonProperty("phases")]
    public Phases Phases { get; set; }

    [JsonProperty("start")]
    public Start Start { get; set; }

    public PlanOutput Parse()
    {
        return JsonConvert.DeserializeObject<PlanOutput>(System.Text.Encoding.UTF8.GetString(Response)) ?? throw new InvalidOperationException();
    }
}

public class Phases
{
    [JsonProperty("build")]
    public Phase Build { get; set; }

    [JsonProperty("install")]
    public Phase Install { get; set; }

    [JsonProperty("setup")]
    public Setup Setup { get; set; }
}

public class Phase
{
    [JsonProperty("dependsOn")]
    public List<string> DependsOn { get; set; }

    [JsonProperty("cmds")]
    public List<string> Cmds { get; set; }

    [JsonProperty("cacheDirectories")]
    public List<string> CacheDirectories { get; set; }
}

public class Setup
{
    [JsonProperty("nixPkgs")]
    public List<string> NixPkgs { get; set; }

    [JsonProperty("nixLibs")]
    public List<string> NixLibs { get; set; }

    [JsonProperty("aptPkgs")]
    public List<string> AptPkgs { get; set; }

    [JsonProperty("nixOverlays")]
    public List<string> NixOverlays { get; set; }

    [JsonProperty("nixpkgsArchive")]
    public string NixpkgsArchive { get; set; }
}

public class Start
{
    [JsonProperty("cmd")]
    public string Cmd { get; set; }

    [JsonProperty("runImage")]
    public string RunImage { get; set; }
}