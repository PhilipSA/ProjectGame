using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class OptionsScreen : SubScreen
    {
        private GraphicOptionsButton _graphicOptionsButton;
        private AudioOptionsButton _audioOptionsButton;
        // Use this for initialization
        protected override void Start()
        {
            _graphicOptionsButton = GetComponentInChildren<GraphicOptionsButton>();
            _graphicOptionsButton.onClick.AddListener(_graphicOptionsButton.OnClick);

            _audioOptionsButton = GetComponentInChildren<AudioOptionsButton>();
            _audioOptionsButton.onClick.AddListener(_audioOptionsButton.OnClick);

            base.Start();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu._parentScreen);
        }
    }
}
