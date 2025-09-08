using UnityEngine;
using UnityEngine.UI;

namespace TheLibrary.UI
{
    public class LibraryButton
    {
        public static void BuildMainMenuButton()
        {
            if (GameObject.Find("modded_Button_MainLibrary") != null)
                return;

            GameObject originalButton = GameObject.Find("Button_Credits");
            if (originalButton == null)
                return;

            GameObject libraryButtonObj = Object.Instantiate(originalButton.gameObject, originalButton.transform.parent);
            if (libraryButtonObj == null)
                return;

            libraryButtonObj.name = "modded_Button_MainLibrary";
            libraryButtonObj.SetActive(true);

            UIUtils.ReplaceText(libraryButtonObj.transform, "Text", "The Library");
            UIUtils.ReplaceSprite(libraryButtonObj.transform, "TokenArt", "images/aspects/secrethistories");
            UIUtils.ReplaceButtonOnClick(libraryButtonObj.GetComponent<Button>().transform, () =>
            {
                LibraryMenu.Toggle();
            });
        }

        public static void BuildPauseMenuButton()
        {
            if (GameObject.Find("modded_Button_PauseLibrary") != null)
                return;

            GameObject originalButton = GameObject.Find("ButtonResume");
            if (originalButton == null)
                return;

            GameObject libraryButtonObj = Object.Instantiate(originalButton.gameObject, originalButton.transform.parent);
            if (libraryButtonObj == null)
                return;

            libraryButtonObj.name = "modded_Button_TheLibrary";
            libraryButtonObj.SetActive(true);

            UIUtils.ReplaceText(libraryButtonObj.transform, "Label", "The Library");
            UIUtils.ReplaceSprite(libraryButtonObj.transform, "TokenArt", "images/aspects/secrethistories");
            UIUtils.ReplaceButtonOnClick(libraryButtonObj.GetComponent<Button>().transform, () =>
            {
                LibraryMenu.Toggle();
            });
        }
    }
}