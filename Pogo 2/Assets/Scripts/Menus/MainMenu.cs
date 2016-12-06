using Assets.Scripts.GUI.OverlayScreens;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        private OverlayScreen currentActiveScreen;
        private StartScreen startScreen;
        private LevelSelectScreen levelSelectScreen;
        // Use this for initialization
        void Start()
        {
            startScreen = (StartScreen)GetComponentInChildren(typeof(StartScreen), true);
            startScreen.Init();

            currentActiveScreen = startScreen;

            levelSelectScreen = (LevelSelectScreen)GetComponentInChildren(typeof(LevelSelectScreen), true);
            levelSelectScreen.Init();

            startScreen.levelSelectButton.onClick.AddListener(OnLevelSelectClick);
        }

        void OnLevelSelectClick()
        {
            ChangeCurrentActiveScreen(levelSelectScreen);
        }

        void ChangeCurrentActiveScreen(OverlayScreen screen)
        {
            currentActiveScreen.SetVisibility(false);
            screen.SetVisibility(true);
            currentActiveScreen = screen;
        }
    }
}
