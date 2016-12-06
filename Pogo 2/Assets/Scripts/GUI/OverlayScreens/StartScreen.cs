using Assets.Scripts.GUI.Buttons;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class StartScreen : OverlayScreen
    {
        public Button levelSelectButton { get; private set; }

        protected override void Start()
        {

        }

        public void Init()
        {
            levelSelectButton = (LevelSelectButton)GetComponentInChildren(typeof(LevelSelectButton), true);
        }
    }
}
