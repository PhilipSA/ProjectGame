using GameObjects;
using Interface.OverlayScreens;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        private StartScreen _startScreen;
        private Menu _menu;

        void Awake()
        {
            _startScreen = CreateGameObject.CreateChildGameObject<StartScreen>(transform).GetComponent<StartScreen>();
            _menu = CreateGameObject.CreateChildGameObject<Menu>(transform).GetComponent<Menu>();
        }

        void Start()
        {
            _menu.SetParentScreen(_startScreen);
            Menu.ChangeCurrentActiveScreen(_startScreen);
        }
    }
}
