using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.OverlayScreens;
using Assets.Scripts.Interface.OverlayScreens.SubScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class Menu : MonoBehaviour
    {
        private static OverlayScreen _currentActiveScreen;
        public static OverlayScreen ParentScreen;
        public static LevelSelectScreen LevelSelectScreen;
        public static OptionsScreen OptionsScreen;
        public static GraphicOptionsSubScreen GraphicOptionsSubScreen;
        public static AudioOptionsSubScreen AudioOptionsSubScreen;
        public static ControlsOptionsSubScreen ControlsOptionsSubScreen;
        public static GameOptionsSubScreen GameOptionsSubScreen;
        // Use this for initialization
        void Start()
        {
            LevelSelectScreen = CreateGameObject.CreateChildGameObject<LevelSelectScreen>(transform).GetComponent<LevelSelectScreen>();
            OptionsScreen = CreateGameObject.CreateChildGameObject<OptionsScreen>(transform).GetComponent<OptionsScreen>();
            GraphicOptionsSubScreen = CreateGameObject.CreateChildGameObject<GraphicOptionsSubScreen>(transform).GetComponent<GraphicOptionsSubScreen>();
            AudioOptionsSubScreen = CreateGameObject.CreateChildGameObject<AudioOptionsSubScreen>(transform).GetComponent<AudioOptionsSubScreen>();
            ControlsOptionsSubScreen = CreateGameObject.CreateChildGameObject<ControlsOptionsSubScreen>(transform).GetComponent<ControlsOptionsSubScreen>();
            GameOptionsSubScreen = CreateGameObject.CreateChildGameObject<GameOptionsSubScreen>(transform).GetComponent<GameOptionsSubScreen>();
        }

        public static void ChangeCurrentActiveScreen(OverlayScreen screen = null)
        {
            Debug.Log(screen);
            if (_currentActiveScreen != null) _currentActiveScreen.SetVisibility(false);
            if(screen != null) screen.SetVisibility(true);
            _currentActiveScreen = screen;
        }

        public void SetParentScreen(OverlayScreen parentScreen)
        {
            ParentScreen = parentScreen;
        }
    }
}
