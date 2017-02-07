using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Viewport : Mask, IRectTransformAble
    {
        public CustomImage CustomImage;
        public RectTransform Content;
        public Item Item;
        public RectTransform RectTransform;

        protected override void Awake()
        {
            CustomImage = gameObject.AddComponent<CustomImage>();
            Content = CreateGameObject.CreateChildGameObject<RectTransform>(transform).GetComponent<RectTransform>();
            Item = CreateGameObject.CreateChildGameObject<Item>(Content.transform).GetComponent<Item>();
            RectTransform = gameObject.GetComponent<RectTransform>();

            SetAnchorsAndPivot(new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1));
            RectTransform.sizeDelta = new Vector2(18, 0);
        }

        protected override void Start()
        {
            base.Start();
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
