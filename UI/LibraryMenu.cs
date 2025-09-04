using UnityEngine;

using SecretHistories.UI;
using SecretHistories.Infrastructure;

using TheLibrary.Utils;

namespace TheLibrary.UI
{
    public class LibraryMenu
    {     
        private static GameObject menu;
        private static GameObject bookEntries;
        private static GameObject entryTemplate;
        private static CanvasGroupFader menuFader;
        private static CanvasGroupFader modalFader;
        private static bool open = false;

        public static void Toggle()
        {
            if (menu == null) Build();

            open = !open;
            if (menuFader != null) menuFader.Toggle();
            if (modalFader != null) modalFader.Toggle();
        }

        public static void Build()
        {
            if (GameObject.Find("modded_OverlayWindow_TheLibrary") != null)
                return;

            LocalNexus nexus = Watchman.Get<LocalNexus>();
            nexus.HideMenusEvent.RemoveListener(HandleHideMenus);
            nexus.HideMenusEvent.AddListener(HandleHideMenus);

            GameObject achievementsPanel = GameObject.Find("OverlayWindow_Achievements");
            if (achievementsPanel == null)
            {
                Plugin.Logger.LogError("Achievements panel not found :(");
                return;
            }

            menu = Object.Instantiate(achievementsPanel.gameObject, achievementsPanel.transform.parent);
            menu.name = "modded_OverlayWindow_TheLibrary";
            SetClassFields();

            UIUtils.ReplaceText(menu.transform, "TitleText", "The Library");
            UIUtils.ReplaceSprite(menu.transform, "TitleArtwork", "images/aspects/secrethistories");
            UIUtils.ReplaceButtonOnClick(menu.transform.Find("CloseButton"), () =>
            {
                Toggle();
            });

            UIUtils.Destroy(menu.transform, "ScrollContent/Viewport/Content/CategoryHolder");
            UIUtils.Destroy(menu.transform, "ScrollContent/Viewport/Content/HiddenAchievementsInfo");

            BuildBookEntries();

            Plugin.Logger.LogWarning("Build method was called");
        }

        private static void HandleHideMenus()
        {
            if (open) Toggle();
        }

        private static void SetClassFields()
        {
            if (menu == null) return;

            Transform bookEntriesTransform = menu.transform.Find("ScrollContent/Viewport/Content/AchievementsHolder");
            if (bookEntriesTransform != null && bookEntriesTransform.childCount > 0)
            {
                if (entryTemplate == null)
                {
                    entryTemplate = Object.Instantiate(bookEntriesTransform.GetChild(0).gameObject);
                    UIUtils.Destroy(entryTemplate.transform, "UnlockTime");
                }

                bookEntries = bookEntriesTransform.gameObject;
                bookEntries.name = "BookEntries";
            }

            menuFader = menu.GetComponent<CanvasGroupFader>();
            GameObject modal = GameObject.Find("Modal");
            if (modal != null)
                modalFader = modal.GetComponent<CanvasGroupFader>();
        }

        private static void BuildBookEntries()
        {
            if (bookEntries == null) return;

            foreach (Transform child in bookEntries.transform)
            {
                Object.Destroy(child.gameObject);
            }

            foreach (string bookId in Morlands.GetReadBooks())
            {
                CreateSingleEntry("entry_" + bookId, Vagabond.GetBookTitle(bookId), Vagabond.GetLibraryText(bookId), "images/elements/" + bookId);
            }
        }

        private static GameObject CreateSingleEntry(string goName, string bookName, string bookDescription, string spritePath)
        {
            GameObject bookEntry = Object.Instantiate(entryTemplate, bookEntries.transform);
            bookEntry.name = goName;
            bookEntry.SetActive(true);

            UnityEngine.UI.HorizontalLayoutGroup layout = bookEntry.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();
            layout.childControlHeight = true;

            SecretHistories.AchievementEntry book = bookEntry.GetComponent<SecretHistories.AchievementEntry>();
            book.SetTexts(bookName, bookDescription);

            Sprite bookSprite = Resources.Load<Sprite>(spritePath);
            book.SetIcon(bookSprite);

            Transform titleTransform = bookEntry.transform.Find("AchievementText/Title");
            if (titleTransform != null)
            {
                TMPro.TextMeshProUGUI textComponent = titleTransform.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.color = Color.white;
                }
            }

            Transform descTransform = bookEntry.transform.Find("AchievementText/Description");
            if (descTransform != null)
            {
                TMPro.TextMeshProUGUI textComponent = descTransform.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    UnityEngine.UI.ContentSizeFitter sizeFitter = textComponent.GetComponent<UnityEngine.UI.ContentSizeFitter>();
                    if (sizeFitter == null) sizeFitter = textComponent.gameObject.AddComponent<UnityEngine.UI.ContentSizeFitter>();

                    sizeFitter.verticalFit = UnityEngine.UI.ContentSizeFitter.FitMode.PreferredSize;
                    sizeFitter.horizontalFit = UnityEngine.UI.ContentSizeFitter.FitMode.Unconstrained;

                    RectTransform rectTransform = textComponent.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(580f, rectTransform.sizeDelta.y);
                }
            }

            return bookEntry;
        }

    }
}