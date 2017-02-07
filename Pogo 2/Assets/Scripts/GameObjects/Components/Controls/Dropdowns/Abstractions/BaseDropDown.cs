using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts;
using Assets.Scripts.GameObjects.Components.Controls.Text;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Abstractions
{
    public abstract class BaseDropDown : Dropdown, IRectTransformAble
    {
        public RectTransform RectTransform;
        public ControlText Label;
        public CustomImage Arrow;
        public Template Template;

        protected override void Awake()
        {
            Label = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            Arrow = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            Template = CreateGameObject.CreateChildGameObject<Template>(transform).GetComponent<Template>();
            RectTransform = gameObject.GetComponent<RectTransform>();
        }

        protected override void Start()
        {
            Arrow.SetAnchors(new Vector2(1, 0.5f), new Vector2(1, 0.5f));
            Arrow.rectTransform.sizeDelta = new Vector2(20, 20);
            targetGraphic = Arrow;

            template = Template.GetComponent<RectTransform>();
            Label.SetAnchors(new Vector2(0, 0), new Vector2(1, 1));
            Label.rectTransform.sizeDelta = new Vector2(25, 6);

            captionText = Label;

            RectTransform.sizeDelta = new Vector2(160, 30);

            Template.enabled = false;
        }

        public void SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            throw new System.NotImplementedException();
        }

        public void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            throw new System.NotImplementedException();
        }
    }
}
