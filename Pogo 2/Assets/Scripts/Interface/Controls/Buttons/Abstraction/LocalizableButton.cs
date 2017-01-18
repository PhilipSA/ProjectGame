using Assets.Scripts.Interface.Controls.Abstraction;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Buttons.Abstraction
{
    public abstract class LocalizableButton : Button, ILocalizableControl
    {
        public string DisplayText { get; set; }

        protected override void Start()
        {
            GetComponentInChildren<Text>().text = DisplayText;
        }
    }
}
