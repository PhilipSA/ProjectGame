using Assets.Scripts.Interface.OverlayScreens;
using Assets.Scripts.Interface.OverlayScreens.SubScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class Menu : MonoBehaviour
    {
        private static OverlayScreen _currentActiveScreen;
        public static StartScreen StartScreen;
        public static LevelSelectScreen LevelSelectScreen;
        public static OptionsScreen OptionsScreenPrefab;
        public static GraphicOptionsSubScreen GraphicOptionsScreen;
        public static AudioOptionsSubScreen AudioOptionsScreen;
        public static ControlsOptionsSubScreen ControlsOptionsScreen;
        public static GameOptionsSubScreen GameOptionsScreen;
        // Use this for initialization
        void Start()
        {
            StartScreen = (StartScreen)GetComponentInChildren(typeof(StartScreen), true);
            StartScreen.Init();

            _currentActiveScreen = StartScreen;            

            LevelSelectScreen = (LevelSelectScreen)GetComponentInChildren(typeof(LevelSelectScreen), true);
            LevelSelectScreen.Init();

            OptionsScreenPrefab = (OptionsScreen)GetComponentInChildren(typeof(OptionsScreen), true);
            GraphicOptionsScreen = (GraphicOptionsSubScreen)GetComponentInChildren(typeof(GraphicOptionsSubScreen), true);
            AudioOptionsScreen = (AudioOptionsSubScreen)GetComponentInChildren(typeof(AudioOptionsSubScreen), true);
            ControlsOptionsScreen = (ControlsOptionsSubScreen)GetComponentInChildren(typeof(ControlsOptionsSubScreen), true);
            GameOptionsScreen = (GameOptionsSubScreen)GetComponentInChildren(typeof(GameOptionsSubScreen), true);
        }

        public static void ChangeCurrentActiveScreen(OverlayScreen screen)
        {
            _currentActiveScreen.SetVisibility(false);
            Debug.Log(screen.name);
            screen.SetVisibility(true);
            _currentActiveScreen = screen;
        }
    }
}
