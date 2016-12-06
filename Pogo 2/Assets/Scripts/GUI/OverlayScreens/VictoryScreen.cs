using UnityEngine.UI;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class VictoryScreen : OverlayScreen
    {
        public void SetClearingTimeText(string text)
        {
            var textObject = transform.Find("ClearingTimeText");
            var textComponent = textObject.GetComponent<Text>();
            textComponent.text = text;
        }
    }
}
