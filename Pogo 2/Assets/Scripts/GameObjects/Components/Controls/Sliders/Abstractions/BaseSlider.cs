using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Sliders.Parts;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Sliders.Abstractions
{
    public abstract class BaseSlider : Slider, IRectTransformAble
    {
        public RectTransform RectTransform { get; private set; }
        public CustomImage Background { get; private set; }
        public HandleSlideArea HandleSlideArea { get; private set; }
        public FillArea FillArea { get; private set; }

        protected override void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            RectTransform.sizeDelta = new Vector2(200, 50);

            Background = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            Background.Initialize(Resources.Load<Sprite>("UI/Skin/background"), UnityEngine.UI.Image.Type.Sliced, new Vector2(0, 0.25f), new Vector2(1, 0.75f));

            HandleSlideArea = CreateGameObject.CreateChildGameObject<HandleSlideArea>(transform).GetComponent<HandleSlideArea>();
            FillArea = CreateGameObject.CreateChildGameObject<FillArea>(transform).GetComponent<FillArea>();
        }

        protected override void Start()
        {
            targetGraphic = HandleSlideArea.ImageRenderer;
            fillRect = FillArea.ImageRenderer.rectTransform;
            handleRect = HandleSlideArea.ImageRenderer.rectTransform;           
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
