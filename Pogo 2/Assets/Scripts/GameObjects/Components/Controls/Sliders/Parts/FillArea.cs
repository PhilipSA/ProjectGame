using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GameObjects.Components.Controls.Sliders.Parts
{
    public class FillArea : UIBehaviour
    {
        public CustomImage ImageRenderer;
        public RectTransform RectTransform;

        protected override void Awake()
        {
            RectTransform = gameObject.AddComponent<RectTransform>();
            RectTransform.anchorMin = new Vector2(0, 0.25f);
            RectTransform.anchorMax = new Vector2(1, 0.75f);
            RectTransform.sizeDelta = new Vector2(0, 0);

            ImageRenderer = CreateGameObject.CreateChildGameObject<CustomImage>(RectTransform, "Fill").GetComponent<CustomImage>();
            ImageRenderer.Initialize(Resources.Load<Sprite>("UI/Skin/null"), UnityEngine.UI.Image.Type.Sliced);
        }
    }
}
