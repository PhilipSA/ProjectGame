using GameObjects;
using GameObjects.Components.Controls.OptionBox.Graphics;
using Interface.OverlayScreens.SubScreens.Abstraction;
using MainEngineComponents;
using Menus;

namespace Interface.OverlayScreens.SubScreens
{
    public class GraphicOptionsSubScreen : OptionsSubScreen
    {
        private ResolutionOptionBox _resolutionOptionBox;
        private ScreenTypeOptionBox _screenTypeOptionBox;

        protected override void Awake()
        {
            _resolutionOptionBox = CreateGameObject.CreateChildGameObject<ResolutionOptionBox>(transform).GetComponent<ResolutionOptionBox>();
            _screenTypeOptionBox = CreateGameObject.CreateChildGameObject<ScreenTypeOptionBox>(transform).GetComponent<ScreenTypeOptionBox>();
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
        }

        protected override void OnBackButtonClick()
        {
            MenuHelper.GetCurrentMenu().ChangeCurrentActiveScreen(MenuHelper.GetCurrentMenu().OptionsScreen);
        }

        protected override void OnApplyButtonClick()
        {
            MainEngine.GetMainEngine.GraphicsComponent.SetResolution(_resolutionOptionBox.GetSelectedResolution(), _screenTypeOptionBox.IsFullscreen());
        }
    }
}
