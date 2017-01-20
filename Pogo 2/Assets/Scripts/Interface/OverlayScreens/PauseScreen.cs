using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class PauseScreen : OverlayScreen
    {
        private ExitButton _exitButton;

        protected override void Start()
        {
            _exitButton = GetComponentInChildren<ExitButton>();
            _exitButton.onClick.AddListener(_exitButton.OnClickInGame);
            base.Start();
        }
    }
}
