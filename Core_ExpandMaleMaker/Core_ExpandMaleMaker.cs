using BepInEx;
using UnityEngine;
using KKAPI.Chara;
using System.Reflection;
using BepInEx.Configuration;

namespace ExpandMaleMaker {

    [BepInPlugin(GUID, Name, Version)]
    [BepInDependency("marco.kkapi")]
    public partial class ExpandMaleMaker : BaseUnityPlugin {

        public const string Name = "Expand Male Maker";
        public const string GUID = "kokaiinum.KKExpandMaleMaker";
        public const string Version = "1.2.0.0";

        internal static MonoBehaviour instance;

        internal static ConfigEntry<bool> heightEnabled;
        internal static ConfigEntry<bool> hairEnabled;
        internal static ConfigEntry<bool> compatabilityMode;

        private void Awake() {
            heightEnabled = Config.Bind("Config", "Edit Male Height", true, new ConfigDescription("If the male height can be edited. \nRequires passing a loading screen to take effect. \nDisabling this will also prevent any male cards from\nbeing loaded with edited heights.", null, new ConfigurationManagerAttributes { Order = 3 }));

            hairEnabled = Config.Bind("Config", "Edit Male Underhair", true, new ConfigDescription("If the male underhair can be edited. \n Requires passing a loading screen to take effect.", null, new ConfigurationManagerAttributes { Order = 2 }));

            compatabilityMode = Config.Bind("Config", "Compatability Mode", true, new ConfigDescription("When enabled, male characters not specifically saved with edited\nheights will be defaulted to 60 (the default behaviour for the game).", null, new ConfigurationManagerAttributes { IsAdvanced = true, Order = 1 }));

            instance = this;

            Hooks.isDark = typeof(ChaInfo).GetProperty("exType", BindingFlags.Public | BindingFlags.Instance) != null;

            CharacterApi.RegisterExtraBehaviour<MaleHeightCompatabilityController>(GUID);

            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Hooks));
        }
    }

}

