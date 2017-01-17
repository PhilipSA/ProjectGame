using Assets.Scripts.GUI.Buttons;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class StartScreen : OverlayScreen
    {
        public Button LevelSelectButton { get; private set; }

        protected override void Start()
        {

        }

        public void Init()
        {
            LevelSelectButton = (LevelSelectButton)GetComponentInChildren(typeof(LevelSelectButton), true);
        }
    }
}
