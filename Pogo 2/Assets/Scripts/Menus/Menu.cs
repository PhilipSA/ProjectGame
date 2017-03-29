using GameObjects;
using Interface.OverlayScreens.Abstraction;
using Interface.OverlayScreens.SubScreens;
using UnityEngine;

namespace Menus
{
    public class Menu : MonoBehaviour
    {
        private OverlayScreen _currentActiveScreen;
        public OverlayScreen ParentScreen;
        public LevelSelectScreen LevelSelectScreen;
        public OptionsScreen OptionsScreen;
        public GraphicOptionsSubScreen GraphicOptionsSubScreen;
        public AudioOptionsSubScreen AudioOptionsSubScreen;
        public ControlsOptionsSubScreen ControlsOptionsSubScreen;
        public GameOptionsSubScreen GameOptionsSubScreen;

        void Awake()
        {
            LevelSelectScreen = CreateGameObject.CreateChildGameObject<LevelSelectScreen>(transform).GetComponent<LevelSelectScreen>();
            OptionsScreen = CreateGameObject.CreateChildGameObject<OptionsScreen>(transform).GetComponent<OptionsScreen>();
            GraphicOptionsSubScreen = CreateGameObject.CreateChildGameObject<GraphicOptionsSubScreen>(transform).GetComponent<GraphicOptionsSubScreen>();
            AudioOptionsSubScreen = CreateGameObject.CreateChildGameObject<AudioOptionsSubScreen>(transform).GetComponent<AudioOptionsSubScreen>();
            ControlsOptionsSubScreen = CreateGameObject.CreateChildGameObject<ControlsOptionsSubScreen>(transform).GetComponent<ControlsOptionsSubScreen>();
            GameOptionsSubScreen = CreateGameObject.CreateChildGameObject<GameOptionsSubScreen>(transform).GetComponent<GameOptionsSubScreen>();
        }

        public void ChangeCurrentActiveScreen(OverlayScreen screen = null)
        {
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
