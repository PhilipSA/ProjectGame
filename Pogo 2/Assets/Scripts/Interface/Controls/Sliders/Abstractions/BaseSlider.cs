using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Sliders.Parts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Abstractions
{
    public abstract class BaseSlider : Slider
    {
        public Image Background;
        public HandleSlideArea HandleSlideArea;
        public FillArea FillArea;

        protected override void Start()
        {
            Background = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>().GetComponent<Image>();
            Background.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            Background.type = Image.Type.Sliced;          

            HandleSlideArea = CreateGameObject.CreateChildGameObject<HandleSlideArea>(transform).GetComponent<HandleSlideArea>().GetComponent<HandleSlideArea>();
            FillArea = CreateGameObject.CreateChildGameObject<FillArea>(transform).GetComponent<FillArea>().GetComponent<FillArea>();

            targetGraphic = HandleSlideArea.ImageRenderer;
            fillRect = FillArea.ImageRenderer.rectTransform;
            handleRect = HandleSlideArea.ImageRenderer.rectTransform;
        }
    }
}
