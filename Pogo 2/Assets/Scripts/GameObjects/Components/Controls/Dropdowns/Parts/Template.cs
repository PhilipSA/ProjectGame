using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Template : ScrollRect, IRectTransformAble
    {
        public CustomImage CustomImage;
        public Viewport Viewport;
        public Scrollbar Scrollbar;
        public RectTransform RectTransform;

        protected override void Awake()
        {
            CustomImage = gameObject.AddComponent<CustomImage>();
            Viewport = CreateGameObject.CreateChildGameObject<Viewport>(transform).GetComponent<Viewport>();
            Scrollbar = CreateGameObject.CreateChildGameObject<Scrollbar>(transform).GetComponent<Scrollbar>();
            RectTransform = gameObject.GetComponent<RectTransform>();
            SetAnchorsAndPivot(new Vector2(0, 0), new Vector2(1, 0), new Vector2(0.5f, 1));
            RectTransform.sizeDelta = new Vector2(0, 150);
        }

        protected override void Start()
        {
            content = Viewport.Content;
            viewport = Viewport.rectTransform;
            verticalScrollbar = Scrollbar;           
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
