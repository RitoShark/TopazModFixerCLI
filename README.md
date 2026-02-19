## SCOPE
This is standalone .exe version of topaz mod fixer from [cslol-go mod manager](https://github.com/Aurecueil/Cs-lol-go)
Its intended for automatization and/or implementation in other tools, not for manual usage

For more details contact me via discord: aurecueil

## USAGE:
```
  App.exe help                    - Show this help
  App.exe <config.json>           - Run with config file
  App.exe <config.json> -chk      - Check mode only
```
## CONFIG FILE FORMAT (JSON):
```json
  {
    "Character": "Ahri",              // REQUIRED
    "BaseWadPath": [                  // REQUIRED (at least one path)
      "C:\\path\\to\\wad1.wad",
      "C:\\path\\to\\wad2.wad"
    ],
    "SkinNo": 1,
    "Output": "./output",
    "Folder": true,
    // ... other optional settings
  }
```
## AVAILABLE SETTINGS:
```
  Strings:
    Character, Output, GameWadPath, RepathFolder,
    GameHashesPath, ShaderHashesPath, Manifest_145, ManifestDownloaderPath

  Integers:
    SkinNo, HealthbarStyle, SoundOption, AnimOption, BnkVersion

  Booleans:
    VerifyHpBar, InFilePath, ClsAssets, KeepIcons, KillStaticMat,
    SfxEvents, Folder, Binless, SmallMod, SkipCheckup,
    NoSkinni, FixiShape, AllAviable

  Double:
    Percent

  Lists (arrays of strings):
    BaseWadPath, OldLookUp, CharraBlackList
```

> [!IMPORTANT]
> Only specify settings you want to change. Unspecified settings will retain their default values.
