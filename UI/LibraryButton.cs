using UnityEngine;
using UnityEngine.UI;

namespace TheLibrary.UI
{
    public class LibraryButton
    {
        public static void Build()
        {
            if (GameObject.Find("modded_Button_Library") != null)
                return;

            GameObject originalButton = GameObject.Find("Button_Credits");
            if (originalButton == null)
                return;

            GameObject libraryButtonObj = Object.Instantiate(originalButton.gameObject, originalButton.transform.parent);
            if (libraryButtonObj == null)
                return;

            libraryButtonObj.name = "modded_Button_Library";
            libraryButtonObj.SetActive(true);

            UIUtils.ReplaceText(libraryButtonObj.transform, "Text", "The Library");
            UIUtils.ReplaceSprite(libraryButtonObj.transform, "TokenArt", "images/aspects/secrethistories");
            UIUtils.ReplaceButtonOnClick(libraryButtonObj.GetComponent<Button>().transform, () =>
            {
                LibraryMenu.Toggle();
            });
        }
    }
}