using Assets.Scripts.GUI.OverlayScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        private static OverlayScreen _currentActiveScreen;
        public static StartScreen StartScreen;
        public static LevelSelectScreen LevelSelectScreen;
        public static OptionsScreen OptionsScreen;
        // Use this for initialization
        void Start()
        {
            StartScreen = (StartScreen)GetComponentInChildren(typeof(StartScreen), true);
            StartScreen.Init();

            _currentActiveScreen = StartScreen;

            LevelSelectScreen = (LevelSelectScreen)GetComponentInChildren(typeof(LevelSelectScreen), true);
            LevelSelectScreen.Init();

            OptionsScreen = (OptionsScreen)GetComponentInChildren(typeof(OptionsScreen), true);
        }

        public static void ChangeCurrentActiveScreen(OverlayScreen screen)
        {
            _currentActiveScreen.SetVisibility(false);
            screen.SetVisibility(true);
            _currentActiveScreen = screen;
        }
    }
}
