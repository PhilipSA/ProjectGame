using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class VictoryScreen : OverlayScreen
    {
        protected override void Start()
        {
            _canvas = GameObject.Find("VictoryScreen");
            _canvas.SetActive(false);
        }

        public void SetClearingTimeText(string text)
        {
            var textObject = transform.Find("ClearingTimeText");
            var textComponent = textObject.GetComponent<Text>();
            textComponent.text = text;
        }
    }
}
