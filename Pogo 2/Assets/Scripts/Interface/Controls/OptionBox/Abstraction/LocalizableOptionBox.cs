using Assets.Scripts.Interface.Controls.Abstraction;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.OptionBox.Abstraction
{
    public abstract class LocalizeableOptionBox : UIBehaviour, ILocalizableControl
    {
        public string DisplayText { get; set; }
        protected bool ValueChanged;
        protected Text TextComponent;

        protected override void Start()
        {
            TextComponent = GetComponentInChildren<Text>();
            TextComponent.text = DisplayText;
        }

        protected virtual void OnValueChanged(float newValue)
        {
            ValueChanged = true;
        }

    }
}
