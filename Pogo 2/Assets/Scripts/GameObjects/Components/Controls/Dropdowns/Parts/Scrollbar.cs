using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Scrollbar : UnityEngine.UI.Scrollbar, IRectTransformAble
    {
        public CustomImage CustomImage;
        public RectTransform SlidingArea;
        public CustomImage Handle;
        public RectTransform RectTransform;

        protected override void Awake()
        {
            CustomImage = gameObject.AddComponent<CustomImage>();

            SlidingArea = CreateGameObject.CreateChildGameObject<RectTransform>(transform, "Sliding Area").GetComponent<RectTransform>();
            SlidingArea.anchorMin = new Vector2(0, 0);
            SlidingArea.anchorMax = new Vector2(1, 1);
            SlidingArea.pivot = new Vector2(0.5f, 0.5f);

            Handle =
                CreateGameObject.CreateChildGameObject<CustomImage>(SlidingArea.transform, "Handle").GetComponent<CustomImage>();
            Handle.SetAnchorsAndPivot(new Vector2(0, 0), new Vector2(1, 0.2f), new Vector2(0.5f, 0.5f));
            Handle.rectTransform.sizeDelta = new Vector2(-10, -10);

            RectTransform = gameObject.GetComponent<RectTransform>();

            SetAnchorsAndPivot(new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 1));
            RectTransform.sizeDelta = new Vector2(20, 0);
        }

        protected override void Start()
        {
            targetGraphic = Handle;
            handleRect = Handle.rectTransform;
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
