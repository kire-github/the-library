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
            LibraryButton.Build();
            LibraryMenu.Build();
        }
    }
}