using Assets.Scripts.Interface.OverlayScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        private StartScreen _startScreen;
        private Menu _menu;

        void Start()
        {
            _startScreen = GetComponentInChildren<StartScreen>();
            _menu = GetComponentInChildren<Menu>();
            _menu.SetParentScreen(_startScreen);
            Menu.ChangeCurrentActiveScreen(_startScreen);
        }
    }
}
