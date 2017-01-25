using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Abstraction;
using Assets.Scripts.Interface.Controls.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interface.Controls.OptionBox.Abstraction
{
    public abstract class LocalizeableOptionBox : UIBehaviour, ILocalizableControl
    {
        public string DisplayText { get; set; }
        protected bool ValueChanged;
        protected ControlText TextComponent;

        protected override void Start()
        {
            gameObject.AddComponent<RectTransform>();
            TextComponent = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            TextComponent.text = DisplayText;
        }

        protected virtual void OnValueChanged(float newValue)
        {
            ValueChanged = true;
        }

    }
}
