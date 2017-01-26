using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Sliders.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Abstractions
{
    public abstract class BaseSlider : Slider
    {
        public RectTransform RectTransform;
        public Image Background;
        public HandleSlideArea HandleSlideArea;
        public FillArea FillArea;

        protected override void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            RectTransform.sizeDelta = new Vector2(200, 50);

            Background = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>().GetComponent<Image>();
            Background.sprite = Resources.Load("UI/Skin/background") as Sprite;
            Background.type = Image.Type.Sliced;
            Background = (Image)TransformGameObject.SetAnchors(Background, new Vector2(0, 0.25f), new Vector2(1, 0.75f));

            HandleSlideArea = CreateGameObject.CreateChildGameObject<HandleSlideArea>(transform).GetComponent<HandleSlideArea>().GetComponent<HandleSlideArea>();
            HandleSlideArea.RectTransform = TransformGameObject.SetAnchors(HandleSlideArea.RectTransform, new Vector2(0, 0),
                new Vector2(1, 1));

            FillArea = CreateGameObject.CreateChildGameObject<FillArea>(transform).GetComponent<FillArea>().GetComponent<FillArea>();
            //FillArea.RectTransform = TransformGameObject.SetAnchors(FillArea.RectTransform, new Vector2(0, 0.25f),
            //    new Vector2(1, 0.75f));
        }

        protected override void Start()
        {
            targetGraphic = HandleSlideArea.ImageRenderer;
            fillRect = FillArea.ImageRenderer.rectTransform;
            handleRect = HandleSlideArea.ImageRenderer.rectTransform;           
        }
    }
}
