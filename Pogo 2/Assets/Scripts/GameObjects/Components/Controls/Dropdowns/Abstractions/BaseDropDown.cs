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
        public ControlText Label;
        public CustomImage Arrow;
        public Template Template;

        protected override void Awake()
        {
            Label = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            Arrow = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            Template = CreateGameObject.CreateChildGameObject<Template>(transform).GetComponent<Template>();
        }

        protected override void Start()
        {
            targetGraphic = Arrow;
            template = Template.GetComponent<RectTransform>();
            captionText = Label;
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
