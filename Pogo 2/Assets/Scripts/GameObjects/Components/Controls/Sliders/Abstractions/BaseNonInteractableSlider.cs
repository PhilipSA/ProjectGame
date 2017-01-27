using Assets.Scripts.GameObjects.Components.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Sliders.Parts;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Sliders.Abstractions
{
    public class BaseNonInteractableSlider : Slider, IRectTransformAble
    {
        public RectTransform RectTransform { get; private set; }
        public CustomImage Background { get; private set; }
        public FillArea FillArea { get; private set; }

        protected override void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Background = CreateGameObject.CreateChildGameObject<CustomImage>(RectTransform, "Background").GetComponent<CustomImage>();
            Background.Initialize(Resources.Load<Sprite>("UI/Skin/background"), UnityEngine.UI.Image.Type.Sliced, new Vector2(0, 0), new Vector2(1, 1));

            FillArea = CreateGameObject.CreateChildGameObject<FillArea>(RectTransform, "Fill Area").GetComponent<FillArea>();
            base.Awake();
        }

        protected override void Start()
        {
            fillRect = FillArea.ImageRenderer.rectTransform;
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
