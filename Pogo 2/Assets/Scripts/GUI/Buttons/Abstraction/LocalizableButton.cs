using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons.Abstraction
{
    public class LocalizableButton : Button
    {
        public string DisplayText;

        protected override void Start()
        {
            GetComponentInChildren<Text>().text = DisplayText;
        }
    }
}
