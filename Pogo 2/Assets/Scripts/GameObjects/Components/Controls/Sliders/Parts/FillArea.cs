using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Controls.Sliders.Parts
{
    public class FillArea : UIComponent
    {
        public CustomImage ImageRenderer;

        protected override void Awake()
        {
            base.Awake();
            ImageRenderer = CreateGameObject.CreateChildGameObject<CustomImage>(RectTransform, "Fill").GetComponent<CustomImage>();
            ImageRenderer.Initialize(Resources.Load<Sprite>("UI/Skin/fill"), UnityEngine.UI.Image.Type.Sliced);
        }

        protected override void Start()
        {
            SetAnchors(new Vector2(0, 0.25f), new Vector2(1, 0.75f));
            RectTransform.sizeDelta = new Vector2(0, 0);
        }

    }
}
