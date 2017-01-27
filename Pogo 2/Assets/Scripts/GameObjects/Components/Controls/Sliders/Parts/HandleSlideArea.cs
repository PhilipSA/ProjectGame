using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Controls.Sliders.Parts
{
    public class HandleSlideArea : UIComponent
    {
        public CustomImage ImageRenderer;

        protected override void Awake()
        {
            base.Awake();

            ImageRenderer = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            ImageRenderer.Initialize(Resources.Load<Sprite>("UI/Skin/knob"), UnityEngine.UI.Image.Type.Simple);
        }

        protected override void Start()
        {
            ImageRenderer.rectTransform.sizeDelta = new Vector2(20, 0);
            RectTransform.sizeDelta = new Vector2(0, 0);
        }
    }
}
