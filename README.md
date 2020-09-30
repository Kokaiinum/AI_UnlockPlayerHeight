# KK_ExpandMaleMaker
This plugin expands the options in the male character creator for Koikatsu. It allows for the height to be changed, and also gives access to the pubic hair options.

Requires [BepInEx 5](https://github.com/BepInEx/BepInEx/releases) and [KKAPI](https://github.com/IllusionMods/IllusionModdingAPI/releases).

### Installation
Download the latest .dll from the [releases tab](https://github.com/Kokaiinum/KK_ExpandMaleMaker/releases) and place it in the BepInEx\Plugins folder.

### Configuration
If you have the BepInEx Configuration Manager plugin installed, you can set the settings to your liking in the F1 mods/plugins menu. If not, there will be a config file in the BepInEx\config folder.


## Additional Usage Notes
* Just as with the ABMX scale options, H animations **WILL** break if you deviate the male's height too much from the default. The game is not designed with different male heights in mind (obviously, that's why the option is disabled by default).
* **Compatibility mode**: at the suggestion of @DeathWeasel1337, by default this plugin will ignore non-default heights on male characters that have not been saved specifically with those heights while this plugin is enabled. This is to prevent issues like old scenes/cards etc suddenly experiencing male characters loading with different heights. If you don't care about this, it can be disabled in the config settings (requires viewing the advanced options).


## Credits and Thanks
* A big thanks to @Mantas-2155X for creating his AI_UnlockPlayerHeight plugin, the transpilers in this plugin are based on the ones he wrote in his plugin.
* Thanks to DeathWeasel1337/Anon11 for the forethought to suggest compatibility mode.
* Thanks to @ManlyMarco for his improvement of the transpilers.
