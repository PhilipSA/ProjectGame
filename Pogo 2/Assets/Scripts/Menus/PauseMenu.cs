using GameObjects;
using Interface.OverlayScreens;
using UnityEngine;

namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private PauseScreen _pauseScreen;
        private Menu _menu;

        void Start()
        {
            _pauseScreen = CreateGameObject.CreateChildGameObject<PauseScreen>(transform).GetComponent<PauseScreen>();
            _menu = CreateGameObject.CreateChildGameObject<Menu>(transform).GetComponent<Menu>();
            _menu.SetParentScreen(_pauseScreen);
        }

        public void ToggleDisplay(bool toggle)
        {
            Menu.ChangeCurrentActiveScreen(toggle ? _pauseScreen : null);
        }
    }
}
