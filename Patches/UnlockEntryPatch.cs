using HarmonyLib;

using SecretHistories.Entities;
using SecretHistories.UI;
using SecretHistories.States;

using TheLibrary.Utils;
using SecretHistories.Services;

namespace TheLibrary.Patches
{
    public static class UnlockEntryPatch
    {
        [HarmonyPatch(typeof(CompleteState), "Enter")]
        public static void Postfix(Situation situation, CompleteState __instance)
        {
            if (situation == null) return;

            Plugin.Logger.LogWarning($"Found a completed recipe {situation.RecipeId}");

            string bookStudied = GetIdBookStudied(situation.RecipeId);
            if (bookStudied == null) return;

            bool newEntryUnlocked = Morlands.MarkBookAsRead(bookStudied);
            if (newEntryUnlocked)
            {
                Watchman.Get<Concursum>().ShowNotification(new NotificationArgs("The Library Remembers", "Another piece to add to my collection... (You unlocked a new entry in the Library)"));
            }
        }

        private static string GetIdBookStudied(string recipeId)
        {
            Recipe recipe = Watchman.Get<Compendium>().GetEntityById<Recipe>(recipeId);
            if (recipe == null) return null;

            foreach (string elementId in recipe.Requirements.Keys)
            {
                Plugin.Logger.LogWarning($"Requirement {elementId}");
                Element element = Watchman.Get<Compendium>().GetEntityById<Element>(elementId);
                if (element != null && element.Aspects.AspectValue("text") > 0) return element.Id;
            }
            return null;
        }
    }
}