using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Controls.Sliders.Parts
{
    public class HandleSlideArea : MonoBehaviour
    {
        public RectTransform RectTransform;
        public CustomImage ImageRenderer;

        void OnEnable()
        {
            RectTransform = gameObject.AddComponent<RectTransform>();
            RectTransform.sizeDelta = new Vector2(0, 0);

            ImageRenderer = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            ImageRenderer.Initialize(Resources.Load<Sprite>("UI/Skin/Knob"), UnityEngine.UI.Image.Type.Simple);
        }
    }
}
