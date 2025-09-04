using HarmonyLib;
using SecretHistories.Infrastructure;

using TheLibrary.UI;

namespace TheLibrary.Patches
{
    [HarmonyPatch(typeof(MenuScreenController), "UpdateAndShowMenu")]
    public class UIBuilderPatch
    {
        public static void Postfix(MenuScreenController __instance)
        {
            if (__instance == null)
                return;

            try
            {
                LibraryButton.Build();
                LibraryMenu.Build();
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError($"Error in UIBuilderPatch: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}