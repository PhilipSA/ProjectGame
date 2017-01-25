using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
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
