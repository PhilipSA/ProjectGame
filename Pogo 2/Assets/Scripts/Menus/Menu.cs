using Assets.Scripts.Interface.OverlayScreens;
using Assets.Scripts.Interface.OverlayScreens.SubScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class Menu : MonoBehaviour
    {
        private static OverlayScreen _currentActiveScreen;
        public static OverlayScreen _parentScreen;
        public static LevelSelectScreen LevelSelectScreen;
        public static OptionsScreen OptionsScreen;
        public static GraphicOptionsSubScreen GraphicOptionsScreen;
        public static AudioOptionsSubScreen AudioOptionsScreen;
        public static ControlsOptionsSubScreen ControlsOptionsScreen;
        public static GameOptionsSubScreen GameOptionsScreen;
        // Use this for initialization
        void Start()
        {           
            LevelSelectScreen = (LevelSelectScreen)GetComponentInChildren(typeof(LevelSelectScreen), true);
            OptionsScreen = (OptionsScreen)GetComponentInChildren(typeof(OptionsScreen), true);
            GraphicOptionsScreen = (GraphicOptionsSubScreen)GetComponentInChildren(typeof(GraphicOptionsSubScreen), true);
            AudioOptionsScreen = (AudioOptionsSubScreen)GetComponentInChildren(typeof(AudioOptionsSubScreen), true);
            ControlsOptionsScreen = (ControlsOptionsSubScreen)GetComponentInChildren(typeof(ControlsOptionsSubScreen), true);
            GameOptionsScreen = (GameOptionsSubScreen)GetComponentInChildren(typeof(GameOptionsSubScreen), true);
        }

        public static void ChangeCurrentActiveScreen(OverlayScreen screen = null)
        {
            if (_currentActiveScreen != null) _currentActiveScreen.SetVisibility(false);
            Debug.Log(screen);
            if(screen != null) screen.SetVisibility(true);
            _currentActiveScreen = screen;
        }

        public void SetParentScreen(OverlayScreen parentScreen)
        {
            _parentScreen = parentScreen;
        }
    }
}
