using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Sliders.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Abstractions
{
    public class BaseNonInteractableSlider : Slider
    {
        public RectTransform RectTransform;
        public Image Background;
        public FillArea FillArea;

        protected override void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Background = CreateGameObject.CreateChildGameObject<Image>(RectTransform, "Background").GetComponent<Image>();
            Background.sprite = Resources.Load<Sprite>("UI/Skin/background");
            Background.type = Image.Type.Sliced;

            FillArea = CreateGameObject.CreateChildGameObject<FillArea>(RectTransform, "Fill Area").GetComponent<FillArea>();
            base.Awake();
        }

        protected override void Start()
        {
            Background = (Image)TransformGameObject.SetAnchors(Background, new Vector2(0, 0), new Vector2(1, 1));
            Background.rectTransform.sizeDelta = new Vector2(0, 0);
            fillRect = FillArea.ImageRenderer.rectTransform;
            base.Start();
        }
    }
}
