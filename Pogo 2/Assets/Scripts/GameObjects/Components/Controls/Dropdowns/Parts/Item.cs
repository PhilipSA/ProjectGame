using Assets.Scripts.GameObjects.Components.Controls.Text;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Parts
{
    public class Item : Toggle
    {
        public CustomImage ItemBackground;
        public CustomImage ItemCheckmark;
        public ControlText ItemLabel;

        protected override void Awake()
        {
            ItemBackground = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            ItemCheckmark = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
            ItemLabel = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
        }

        protected override void Start()
        {
            targetGraphic = ItemBackground;
            graphic = ItemCheckmark;
        }
    }
}
