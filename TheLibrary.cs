using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;

using TheLibrary.Patches;

namespace TheLibrary
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("cultistsimulator.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        internal static ConfigEntry<string> ReadBooksConfig;
        internal static Harmony harmony;

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is being loaded");

            ReadBooksConfig = Config.Bind("TheLibrary", "read_books", "", "CSV of book IDs that are considered read");

            harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(UnlockEntryPatch));
            harmony.PatchAll(typeof(UIBuilderPatch));

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded successfully");
        }

    }
}
