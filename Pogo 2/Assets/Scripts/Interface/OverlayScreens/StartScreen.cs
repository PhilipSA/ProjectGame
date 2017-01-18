using Assets.Scripts.Interface.Controls.Buttons;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens
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
