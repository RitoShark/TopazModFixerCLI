using ModManager;

// Initialize the fixer
Repatheruwu Fixer = new Repatheruwu();

// 1. Check for Help flags immediately
if (args.Length == 0 || args.Any(a => a.Equals("help", StringComparison.OrdinalIgnoreCase) ||
                                      a.Equals("--help", StringComparison.OrdinalIgnoreCase) ||
                                      a.Equals("-h", StringComparison.OrdinalIgnoreCase) ||
                                      a.Equals("?", StringComparison.OrdinalIgnoreCase)))
{
    PrintHelp();
    return; // Stop execution
}
bool check = false;
// 2. Iterate over arguments
foreach (string arg in args)
{
    // Split Key=Value
    var parts = arg.Split(new[] { '=' }, 2);
    if (parts.Length != 2) continue;

    string key = parts[0].TrimStart('-');
    string value = parts[1];

    try
    {
        switch (key.ToLower())
        {
            // Strings
            case "champion":
                Fixer.Settings.Character = value;
                break;
            case "output":
                Fixer.Settings.outputDir = value;
                break;
            case "gamewadpath":
                Fixer.Settings.WADpath = value;
                break;
            case "repathfolder":
                Fixer.Settings.repath_path_path = value;
                break;
            case "gamehashespath":
                Fixer.Settings.gamehashes_path = value;
                break;
            case "shaderhashespath":
                Fixer.Settings.shaderhashes_path = value;
                break;
            case "manifest_145":
                Fixer.Settings.manifest_145 = value;
                break;
            case "manifestdownloaderpath": // Fixed typo in switch case string
                Fixer.Settings.ManfiestDL = value;
                break;

            // Integers
            case "skinno":
                Fixer.Settings.skinNo = int.Parse(value);
                break;
            case "healthbarstyle":
                Fixer.Settings.HealthbarStyle = int.Parse(value);
                break;
            case "soundoption":
                Fixer.Settings.SoundOption = int.Parse(value);
                break;
            case "animoption":
                Fixer.Settings.AnimOption = int.Parse(value);
                break;
            case "bnk_version":
                Fixer.Settings.bnk_version = uint.Parse(value);
                break;

            // Booleans
            // Booleans
            case "chek":
                check = bool.Parse(value);
                break;
            case "verifyhpbar":
                Fixer.Settings.verifyHpBar = bool.Parse(value);
                break;
            case "in_file_path":
                Fixer.Settings.in_file_path = bool.Parse(value);
                break;
            case "cls_assets":
                Fixer.Settings.cls_assets = bool.Parse(value);
                break;
            case "keep_icons":
                Fixer.Settings.keep_Icons = bool.Parse(value);
                break;
            case "killstaticmat":
                Fixer.Settings.KillStaticMat = bool.Parse(value);
                break;
            case "sfx_events":
                Fixer.Settings.sfx_events = bool.Parse(value);
                break;
            case "folder":
                Fixer.Settings.folder = bool.Parse(value);
                break;
            case "binless":
                Fixer.Settings.binless = bool.Parse(value);
                break;
            case "smallmod":
                Fixer.Settings.SmallMod = bool.Parse(value);
                break;
            case "skipcheckup":
                Fixer.Settings.SkipCheckup = bool.Parse(value);
                break;
            case "noskinni":
                Fixer.Settings.noskinni = bool.Parse(value);
                break;
            case "fixishape":
                Fixer.Settings.FixiShape = bool.Parse(value);
                break;
            case "allaviable":
                Fixer.Settings.AllAviable = bool.Parse(value);
                break;

            // Doubles
            case "percent":
                Fixer.Settings.percent = double.Parse(value);
                break;

            // Lists
            case "base_wad_path":
                Fixer.Settings.base_wad_path = value.Split(',').Select(s => s.Trim()).ToList();
                break;
            case "oldlookup":
                Fixer.Settings.OldLookUp = value.Split(',').Select(s => s.Trim()).ToList();
                break;
            case "charrablacklist":
                Fixer.Settings.CharraBlackList = value.Split(',').Select(s => s.Trim()).ToList();
                break;

            default:
                Console.WriteLine($"Warning: Unknown argument '{key}'");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine($"Error: Invalid format for argument '{key}' with value '{value}'");
    }
}

// 3. Validate Mandatory Arguments
if (string.IsNullOrEmpty(Fixer.Settings.Character))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Error: The 'Character' argument is required.");
    Console.ResetColor();
    Console.WriteLine("Use --help to see usage instructions.");
    return;
}
static void PrintHelp()
{
    Console.WriteLine("--- Repatheruwu Argument Helper ---");
    Console.WriteLine("Usage: App.exe key=value key2=value2");
    Console.WriteLine("Example: App.exe Character=Ahri skinNo=1 folder=false");
    Console.WriteLine("");
    Console.WriteLine("{0,-25} | {1,-10} | {2}", "Argument Key", "Type", "Default Value");
    Console.WriteLine(new string('-', 80));

    PrintRow("chek", "bool", "false (true value will make exe only output skin values expected for given skin)");

    // Strings
    PrintRow("Character", "string", "none (REQUIRED)");
    PrintRow("output", "string", ".");
    PrintRow("gamewadpath", "string", @"C:\Riot Games\League of Legends\Game\DATA\FINAL");
    PrintRow("repathfolder", "string", "(auto-generated based on skin)");
    PrintRow("gamehashespath", "string", @"cslol-tools\hashes.game.txt");
    PrintRow("shaderhashespath", "string", @"cslol-tools\hashes.shaders.txt");
    PrintRow("manifest_145", "string", "https://.../998BEDBD1E22BD5E.manifest");
    PrintRow("manfiestdownloaderpath", "string", @"cslol-tools\ManifestDownloader.exe");

    // Ints / UInts
    PrintRow("skinNo", "int", "0");
    PrintRow("HealthbarStyle", "int", "12");
    PrintRow("SoundOption", "int", "0");
    PrintRow("AnimOption", "int", "0");
    PrintRow("bnk_version", "uint", "145");

    // Bools
    PrintRow("verifyHpBar", "bool", "true");
    PrintRow("in_file_path", "bool", "true");
    PrintRow("cls_assets", "bool", "true");
    PrintRow("keep_Icons", "bool", "true");
    PrintRow("KillStaticMat", "bool", "false");
    PrintRow("sfx_events", "bool", "false");
    PrintRow("folder", "bool", "true");
    PrintRow("binless", "bool", "false");
    PrintRow("SmallMod", "bool", "true");
    PrintRow("SkipCheckup", "bool", "false");
    PrintRow("noskinni", "bool", "true");
    PrintRow("FixiShape", "bool", "true");
    PrintRow("AllAviable", "bool", "true");

    // Double
    PrintRow("percent", "double", "80");

    // Lists
    Console.WriteLine("");
    Console.WriteLine("--- Lists (Comma separated values) ---");
    PrintRow("base_wad_path", "list", "(empty)");
    PrintRow("OldLookUp", "list", "(empty)");
    PrintRow("CharraBlackList", "list", "viegowraith");

    Console.WriteLine("");
}

static void PrintRow(string key, string type, string val)
{
    Console.WriteLine("{0,-25} | {1,-10} | {2}", key, type, val);
}
// 4. Run Fixer
try
{
    if (check)
    {
        var (i, b) = Fixer.getSkinInts(Fixer.Settings.Character);
        Console.WriteLine($"skin:{i}");
        Console.WriteLine($"binless:{b}");
    }
    else
    {
        Fixer.FixiniYoursSkini();
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Critical Error: {ex.Message}");
    Console.ResetColor();
}