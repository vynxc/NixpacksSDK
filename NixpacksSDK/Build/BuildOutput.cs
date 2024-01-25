namespace NixpacksSDK.Build;

public class BuildOutput
{
    public string Response { get; set; }

    public bool IsBrokenImage { get; set; }

    public string BuildError { get; set; }

    public string ImageName { get; set; }

    public string Language { get; set; }

    public string Install { get; set; }

    public string Build { get; set; }

    public string Start { get; set; }


    public void Parse()
    {
        if (Response == null || Response.Length == 0)
        {
            throw new ArgumentException("Response is empty");
        }

        var output = Response;
        var lines = output.Split('\n');

        bool save = false;
        bool skipFirst = false;

        foreach (var line in lines)
        {
            if (line.StartsWith("║ setup"))
            {
                Language = ProcessString(line, "║ setup");
            }
            else if (line.StartsWith("║ install"))
            {
                Install = ProcessString(line, "║ install");
            }
            else if (line.StartsWith("║ build"))
            {
                Build = ProcessString(line, "║ build");
            }
            else if (line.StartsWith("║ start"))
            {
                Start = ProcessString(line, "║ start");
            }
            else if (line.StartsWith("  docker run -it "))
            {
                ImageName = line.Replace("  docker run -it ", "").Trim();
            }

            if (IsBrokenImage)
            {
                if (line == "------")
                {
                    save = !save;
                    continue;
                }

                if (save)
                {
                    if (!skipFirst)
                    {
                        skipFirst = true;
                        continue;
                    }

                    BuildError += line + "\n";
                }
            }
        }
    }

    private string ProcessString(string str, string replace)
    {
        str = str.Replace(replace, "");
        str = str.Split('│')[1];
        str = str.Replace("║", "");
        return str.Trim();
    }
}