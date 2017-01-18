using Assets.Scripts.Interface.OverlayScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private PauseScreen _pauseScreen;
        private Menu _menu;

        void Start()
        {
            _pauseScreen = GetComponentInChildren<PauseScreen>();
            _menu = GetComponentInChildren<Menu>();
            _menu.SetParentScreen(_pauseScreen);
        }

        public void ToggleDisplay(bool toggle)
        {
            Menu.ChangeCurrentActiveScreen(toggle ? _pauseScreen : null);
        }
    }
}
