using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Template : ScrollRect
    {
        public CustomImage CustomImage;
        public Viewport Viewport;
        public Scrollbar Scrollbar;

        protected override void Awake()
        {
            CustomImage = gameObject.AddComponent<CustomImage>();
            Viewport = CreateGameObject.CreateChildGameObject<Viewport>(transform).GetComponent<Viewport>();
            Scrollbar = CreateGameObject.CreateChildGameObject<Scrollbar>(transform).GetComponent<Scrollbar>();
        }

        protected override void Start()
        {
            content = Viewport.Content;
            viewport = Viewport.rectTransform;
            verticalScrollbar = Scrollbar;
        }
    }
}
