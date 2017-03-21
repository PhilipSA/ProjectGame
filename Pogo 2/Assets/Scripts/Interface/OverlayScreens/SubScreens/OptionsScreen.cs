using GameObjects;
using GameObjects.Components.Controls.Buttons;
using Interface.OverlayScreens.SubScreens.Abstraction;
using Menus;

namespace Interface.OverlayScreens.SubScreens
{
    public class OptionsScreen : SubScreen
    {
        public GraphicOptionsButton GraphicOptionsButton;
        public AudioOptionsButton AudioOptionsButton;

        // Use this for initialization
        protected override void Awake()
        {
            GraphicOptionsButton = CreateGameObject.CreateChildGameObject<GraphicOptionsButton>(transform).GetComponent<GraphicOptionsButton>();
            AudioOptionsButton = CreateGameObject.CreateChildGameObject<AudioOptionsButton>(transform).GetComponent<AudioOptionsButton>();

            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.ParentScreen);
        }
    }
}
