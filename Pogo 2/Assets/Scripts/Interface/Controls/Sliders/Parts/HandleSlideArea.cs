using Assets.Scripts.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Parts
{
    public class HandleSlideArea : MonoBehaviour
    {
        public RectTransform RectTransform;
        public Image ImageRenderer;

        void OnEnable()
        {
            RectTransform = gameObject.AddComponent<RectTransform>();
            ImageRenderer = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();
            ImageRenderer.sprite = Resources.Load("UI/Skin/Knob") as Sprite;
            ImageRenderer.type = Image.Type.Simple;
        }
    }
}
