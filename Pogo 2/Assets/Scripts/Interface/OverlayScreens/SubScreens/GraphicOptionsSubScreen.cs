using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class GraphicOptionsSubScreen : OptionsSubScreen
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.OptionsScreen);
        }
    }
}
