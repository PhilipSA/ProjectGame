using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Viewport : Mask
    {
        public CustomImage CustomImage;
        public RectTransform Content;
        public Item Item;

        protected override void Awake()
        {
            CustomImage = gameObject.AddComponent<CustomImage>();
            Content = CreateGameObject.CreateChildGameObject<RectTransform>(transform).GetComponent<RectTransform>();
            Item = CreateGameObject.CreateChildGameObject<Item>(Content.transform).GetComponent<Item>();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}
