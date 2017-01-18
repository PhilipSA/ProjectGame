using Assets.Scripts.Interface.Controls.OptionBox;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class AudioOptionsSubScreen : OptionsSubScreen
    {
        private MusicVolumeOptionBox _musicVolumeOptionBox;

        protected override void Start()
        {
            _musicVolumeOptionBox = (MusicVolumeOptionBox)GetComponentInChildren(typeof(MusicVolumeOptionBox));
            base.Start();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.OptionsScreen);
        }
    }
}
