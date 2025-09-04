using SecretHistories.Entities;
using SecretHistories.UI;

namespace TheLibrary.Utils
{
    public static class Vagabond
    {
        public static string GetBookTitle(string bookId)
        {
            Element book = Watchman.Get<Compendium>().GetEntityById<Element>(bookId);
            return book?.Label;
        }

        public static string GetLibraryText(string bookId)
        {
            return GetBookDescription(bookId) + "\n\n" + GetRecipeTexts(bookId);
        }


        private static string GetBookDescription(string bookId)
        {
            Element book = Watchman.Get<Compendium>().GetEntityById<Element>(bookId);
            return book?.Description;
        }

        private static string GetRecipeTexts(string bookId)
        {
            foreach (Recipe recipe in Watchman.Get<Compendium>().GetEntitiesAsList<Recipe>())
            {
                if (recipe.Requirements.ContainsKey(bookId))
                {
                    return recipe.StartDescription + "\n\n" + recipe.Description;
                }
            }
            return "";
        }
    }
}