using UnityEngine;

namespace TheLibrary.UI
{
    public static class UIUtils
    {
        public static void Destroy(Transform parent, string name)
        {
            Transform destroyTarget = parent.Find(name);
            if (destroyTarget != null)
            {
                Object.Destroy(destroyTarget.gameObject);
            }
        }

        public static void ReplaceText(Transform parent, string name, string newText)
        {
            Transform textTransform = parent.Find(name);
            if (textTransform == null) return;

            TMPro.TextMeshProUGUI titleText = textTransform.GetComponent<TMPro.TextMeshProUGUI>();
            if (titleText == null) return;

            titleText.text = newText;
        }

        public static void ReplaceSprite(Transform parent, string name, string pathToSprite)
        {
            Transform spriteTransform = parent.Find(name);
            if (spriteTransform == null) return;

            UnityEngine.UI.Image titleSprite = spriteTransform.GetComponent<UnityEngine.UI.Image>();
            if (titleSprite == null) return;

            Sprite newSprite = Resources.Load<Sprite>(pathToSprite);
            titleSprite.sprite = newSprite;
        }

        public static void ReplaceButtonOnClick(Transform parent, UnityEngine.Events.UnityAction onClick)
        {
            if (parent == null) return;
            
            UnityEngine.UI.Button button = parent.GetComponent<UnityEngine.UI.Button>();
            if (button == null) return;

            button.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            button.onClick.AddListener(onClick);
        }
    }
}