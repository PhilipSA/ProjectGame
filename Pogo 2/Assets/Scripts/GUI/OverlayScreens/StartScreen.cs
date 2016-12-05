using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class StartScreen : OverlayScreen
    {
        public Button levelSelectButton { get; private set; }
        private LevelSelectScreen _levelSelectScreen;

        void Awake()
        {
            _levelSelectScreen = (LevelSelectScreen)GetComponentInParent(typeof(LevelSelectScreen));
            levelSelectButton = transform.Find("LevelSelectButton").GetComponent<Button>();
        }
    }
}
