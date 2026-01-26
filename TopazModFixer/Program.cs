using ModManager;
using System.Text.Json;
using System.Text.Json.Serialization;

// Parse arguments
if (args.Length == 0 || IsHelpRequested(args))
{
    PrintHelp();
    return;
}

string configPath = args[0];
bool checkMode = args.Length > 1 && args[1].Equals("-chk", StringComparison.OrdinalIgnoreCase);

// Validate config file exists
if (!File.Exists(configPath))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: Config file not found: {configPath}");
    Console.ResetColor();
    return;
}

// Initialize fixer
Repatheruwu Fixer = new Repatheruwu();

// Load and apply configuration
try
{
    string jsonContent = File.ReadAllText(configPath);
    var config = JsonSerializer.Deserialize(jsonContent, SourceGenerationContext.Default.ConfigFile);

    if (config == null)
    {
        throw new Exception("Failed to deserialize config file");
    }

    // Apply configuration
    // Strings
    if (config.Character != null) Fixer.Settings.Character = config.Character;
    if (config.Output != null) Fixer.Settings.outputDir = config.Output;
    if (config.GameWadPath != null) Fixer.Settings.WADpath = config.GameWadPath;
    if (config.RepathFolder != null) Fixer.Settings.repath_path_path = config.RepathFolder;
    if (config.GameHashesPath != null) Fixer.Settings.gamehashes_path = config.GameHashesPath;
    if (config.ShaderHashesPath != null) Fixer.Settings.shaderhashes_path = config.ShaderHashesPath;
    if (config.Manifest_145 != null) Fixer.Settings.manifest_145 = config.Manifest_145;
    if (config.ManifestDownloaderPath != null) Fixer.Settings.ManfiestDL = config.ManifestDownloaderPath;

    // Integers
    if (config.SkinNo.HasValue) Fixer.Settings.skinNo = config.SkinNo.Value;
    if (config.HealthbarStyle.HasValue) Fixer.Settings.HealthbarStyle = config.HealthbarStyle.Value;
    if (config.SoundOption.HasValue) Fixer.Settings.SoundOption = config.SoundOption.Value;
    if (config.AnimOption.HasValue) Fixer.Settings.AnimOption = config.AnimOption.Value;
    if (config.BnkVersion.HasValue) Fixer.Settings.bnk_version = config.BnkVersion.Value;

    // Booleans
    if (config.VerifyHpBar.HasValue) Fixer.Settings.verifyHpBar = config.VerifyHpBar.Value;
    if (config.InFilePath.HasValue) Fixer.Settings.in_file_path = config.InFilePath.Value;
    if (config.ClsAssets.HasValue) Fixer.Settings.cls_assets = config.ClsAssets.Value;
    if (config.KeepIcons.HasValue) Fixer.Settings.keep_Icons = config.KeepIcons.Value;
    if (config.KillStaticMat.HasValue) Fixer.Settings.KillStaticMat = config.KillStaticMat.Value;
    if (config.SfxEvents.HasValue) Fixer.Settings.sfx_events = config.SfxEvents.Value;
    if (config.Folder.HasValue) Fixer.Settings.folder = config.Folder.Value;
    if (config.Binless.HasValue) Fixer.Settings.binless = config.Binless.Value;
    if (config.SmallMod.HasValue) Fixer.Settings.SmallMod = config.SmallMod.Value;
    if (config.SkipCheckup.HasValue) Fixer.Settings.SkipCheckup = config.SkipCheckup.Value;
    if (config.NoSkinni.HasValue) Fixer.Settings.noskinni = config.NoSkinni.Value;
    if (config.FixiShape.HasValue) Fixer.Settings.FixiShape = config.FixiShape.Value;
    if (config.AllAviable.HasValue) Fixer.Settings.AllAviable = config.AllAviable.Value;

    // Double
    if (config.Percent.HasValue) Fixer.Settings.percent = config.Percent.Value;

    // Lists
    if (config.BaseWadPath != null) Fixer.Settings.base_wad_path = config.BaseWadPath;
    if (config.OldLookUp != null) Fixer.Settings.OldLookUp = config.OldLookUp;
    if (config.CharraBlackList != null) Fixer.Settings.CharraBlackList = config.CharraBlackList;

    // Validate required fields
    if (string.IsNullOrEmpty(Fixer.Settings.Character))
    {
        throw new Exception("'Character' is required in config file");
    }

    if (Fixer.Settings.base_wad_path == null || Fixer.Settings.base_wad_path.Count == 0)
    {
        throw new Exception("At least one path in 'BaseWadPath' is required in config file");
    }
}
catch (JsonException ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error parsing JSON: {ex.Message}");
    Console.ResetColor();
    return;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error loading config: {ex.Message}");
    Console.ResetColor();
    return;
}

// Run the appropriate mode
try
{
    if (checkMode)
    {
        var (skinInt, binless) = Fixer.getSkinInts(Fixer.Settings.Character);
        Console.WriteLine($"skin:{skinInt}");
        Console.WriteLine($"binless:{binless}");
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

// Helper methods
static bool IsHelpRequested(string[] args)
{
    return args.Any(a => a.Equals("help", StringComparison.OrdinalIgnoreCase) ||
                        a.Equals("--help", StringComparison.OrdinalIgnoreCase) ||
                        a.Equals("-h", StringComparison.OrdinalIgnoreCase) ||
                        a.Equals("?", StringComparison.OrdinalIgnoreCase));
}

static void PrintHelp()
{
    Console.WriteLine("=== Repatheruwu - JSON Configuration Tool ===");
    Console.WriteLine();
    Console.WriteLine("USAGE:");
    Console.WriteLine("  App.exe help                    - Show this help");
    Console.WriteLine("  App.exe <config.json>           - Run with config file");
    Console.WriteLine("  App.exe <config.json> -chk      - Check mode only");
    Console.WriteLine();
    Console.WriteLine("CONFIG FILE FORMAT (JSON):");
    Console.WriteLine("  {");
    Console.WriteLine("    \"Character\": \"Ahri\",              // REQUIRED");
    Console.WriteLine("    \"BaseWadPath\": [                  // REQUIRED (at least one path)");
    Console.WriteLine("      \"C:\\\\path\\\\to\\\\wad1.wad\",");
    Console.WriteLine("      \"C:\\\\path\\\\to\\\\wad2.wad\"");
    Console.WriteLine("    ],");
    Console.WriteLine("    \"SkinNo\": 1,");
    Console.WriteLine("    \"Output\": \"./output\",");
    Console.WriteLine("    \"Folder\": true,");
    Console.WriteLine("    // ... other optional settings");
    Console.WriteLine("  }");
    Console.WriteLine();
    Console.WriteLine("AVAILABLE SETTINGS:");
    Console.WriteLine();
    Console.WriteLine("  Strings:");
    Console.WriteLine("    Character, Output, GameWadPath, RepathFolder,");
    Console.WriteLine("    GameHashesPath, ShaderHashesPath, Manifest_145, ManifestDownloaderPath");
    Console.WriteLine();
    Console.WriteLine("  Integers:");
    Console.WriteLine("    SkinNo, HealthbarStyle, SoundOption, AnimOption, BnkVersion");
    Console.WriteLine();
    Console.WriteLine("  Booleans:");
    Console.WriteLine("    VerifyHpBar, InFilePath, ClsAssets, KeepIcons, KillStaticMat,");
    Console.WriteLine("    SfxEvents, Folder, Binless, SmallMod, SkipCheckup,");
    Console.WriteLine("    NoSkinni, FixiShape, AllAviable");
    Console.WriteLine();
    Console.WriteLine("  Double:");
    Console.WriteLine("    Percent");
    Console.WriteLine();
    Console.WriteLine("  Lists (arrays of strings):");
    Console.WriteLine("    BaseWadPath, OldLookUp, CharraBlackList");
    Console.WriteLine();
    Console.WriteLine("NOTE: Only specify settings you want to change. Unspecified settings");
    Console.WriteLine("      will retain their default values.");
}

// Configuration class for JSON deserialization
public class ConfigFile
{
    // Strings
    public string? Character { get; set; }
    public string? Output { get; set; }
    public string? GameWadPath { get; set; }
    public string? RepathFolder { get; set; }
    public string? GameHashesPath { get; set; }
    public string? ShaderHashesPath { get; set; }
    public string? Manifest_145 { get; set; }
    public string? ManifestDownloaderPath { get; set; }

    // Integers
    public int? SkinNo { get; set; }
    public int? HealthbarStyle { get; set; }
    public int? SoundOption { get; set; }
    public int? AnimOption { get; set; }
    public uint? BnkVersion { get; set; }

    // Booleans
    public bool? VerifyHpBar { get; set; }
    public bool? InFilePath { get; set; }
    public bool? ClsAssets { get; set; }
    public bool? KeepIcons { get; set; }
    public bool? KillStaticMat { get; set; }
    public bool? SfxEvents { get; set; }
    public bool? Folder { get; set; }
    public bool? Binless { get; set; }
    public bool? SmallMod { get; set; }
    public bool? SkipCheckup { get; set; }
    public bool? NoSkinni { get; set; }
    public bool? FixiShape { get; set; }
    public bool? AllAviable { get; set; }

    // Double
    public double? Percent { get; set; }

    // Lists
    public List<string>? BaseWadPath { get; set; }
    public List<string>? OldLookUp { get; set; }
    public List<string>? CharraBlackList { get; set; }
}

// JSON Source Generator Context
[JsonSerializable(typeof(ConfigFile))]
[JsonSourceGenerationOptions(
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}