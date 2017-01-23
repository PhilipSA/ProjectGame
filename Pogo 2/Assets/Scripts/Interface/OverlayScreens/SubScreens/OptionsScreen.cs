using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class OptionsScreen : SubScreen
    {
        public GraphicOptionsButton GraphicOptionsButton;
        public AudioOptionsButton AudioOptionsButton;

        // Use this for initialization
        protected override void Start()
        {
            LayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            GraphicOptionsButton = CreateGameObject.CreateChildGameObject<GraphicOptionsButton>(transform).GetComponent<GraphicOptionsButton>();
            AudioOptionsButton = CreateGameObject.CreateChildGameObject<AudioOptionsButton>(transform).GetComponent<AudioOptionsButton>();

            base.Start();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.ParentScreen);
        }
    }
}
