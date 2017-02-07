using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Text;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Item : Toggle, IRectTransformAble
    {
        public CustomImage ItemBackground;
        public CustomImage ItemCheckmark;
        public ControlText ItemLabel;
        public RectTransform RectTransform;

        protected override void Awake()
        {
            ItemBackground = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            ItemCheckmark = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            ItemLabel = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();

            RectTransform = gameObject.GetComponent<RectTransform>();

            SetAnchorsAndPivot(new Vector2(0, 0.5f), new Vector2(1, 0.5f), new Vector2(0.5f, 0.5f));
            RectTransform.sizeDelta = new Vector2(0, 20);
        }

        protected override void Start()
        {
            targetGraphic = ItemBackground;
            graphic = ItemCheckmark;
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
