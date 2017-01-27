using Assets.Scripts.GameObjects.Components.Controls.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GameObjects.Components.Controls.OptionBox.Abstraction
{
    public abstract class LocalizeableOptionBox : UIBehaviour, ILocalizableControl
    {
        public string DisplayText { get; set; }
        protected bool ValueChanged;
        protected ControlText TextComponent;
        protected RectTransform RectTransform;

        protected override void Start()
        {
            RectTransform = gameObject.AddComponent<RectTransform>();
            TextComponent = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            TextComponent.text = DisplayText;
        }

        protected virtual void OnValueChanged(float newValue)
        {
            ValueChanged = true;
        }

        public void SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
        }

        public void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
            RectTransform.pivot = pivot;
        }
    }
}
