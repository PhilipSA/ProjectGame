using Assets.Scripts.GameObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Parts
{
    public class FillArea : UIBehaviour
    {
        public Image ImageRenderer;
        public RectTransform RectTransform;

        protected override void Awake()
        {
            RectTransform = gameObject.AddComponent<RectTransform>();
            RectTransform.anchorMin = new Vector2(0, 0.25f);
            RectTransform.anchorMax = new Vector2(1, 0.75f);
            RectTransform.sizeDelta = new Vector2(0, 0);

            ImageRenderer = CreateGameObject.CreateChildGameObject<Image>(RectTransform, "Fill").GetComponent<Image>();
            ImageRenderer.sprite = Resources.Load<Sprite>("UI/Skin/background");
            ImageRenderer.type = Image.Type.Sliced;
            ImageRenderer.rectTransform.sizeDelta = new Vector2(0, 0);
        }
    }
}
