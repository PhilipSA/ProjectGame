using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Scrollbar : UnityEngine.UI.Scrollbar
    {
        public CustomImage CustomImage;
        public RectTransform SlidingArea;
        public CustomImage Handle;

        protected override void Awake()
        {
            CustomImage = gameObject.AddComponent<CustomImage>();
            SlidingArea = CreateGameObject.CreateChildGameObject<RectTransform>(transform).GetComponent<RectTransform>();
            Handle =
                CreateGameObject.CreateChildGameObject<CustomImage>(SlidingArea.transform).GetComponent<CustomImage>();
        }

        protected override void Start()
        {
            targetGraphic = Handle;
            handleRect = Handle.rectTransform;
        }
    }
}
