using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class GraphicOptionsSubScreen : OptionsSubScreen
    {
        void Awake()
        {
            BackButton = GetComponentInChildren<BackButton>();
            SetBackButtonListener(OnBackButtonClick);
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.OptionsScreenPrefab);
        }
    }
}
