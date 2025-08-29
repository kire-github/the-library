using System.Collections.Generic;
using System.Linq;

namespace TheLibrary.Utils
{
    public static class Morlands
    {
        private static HashSet<string> readBooks = new HashSet<string>();
        private static bool loaded = false;

        public static void LoadConfig()
        {
            if (loaded) return;

            string savedBooks = Plugin.ReadBooksConfig.Value;
            if (!string.IsNullOrEmpty(savedBooks))
            {
                readBooks = new HashSet<string>(savedBooks.Split(',').Where(b => !string.IsNullOrWhiteSpace(b)));
            }
            loaded = true;

            Plugin.Logger.LogInfo($"Loaded {readBooks.Count} read books from config.");
        }

        public static void SaveConfig()
        {
            string bookString = string.Join(",", readBooks);
            Plugin.ReadBooksConfig.Value = bookString;
        }

        public static void MarkBookAsRead(string bookId)
        {
            LoadConfig();
            if (!string.IsNullOrEmpty(bookId) && !readBooks.Contains(bookId))
            {
                readBooks.Add(bookId);
                SaveConfig();
            }
        }

        public static HashSet<string> GetReadBooks()
        {
            LoadConfig();
            return readBooks;
        }
    }
}