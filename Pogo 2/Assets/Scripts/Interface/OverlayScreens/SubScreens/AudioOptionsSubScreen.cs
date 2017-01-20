using Assets.Scripts.Interface.Controls.OptionBox;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class AudioOptionsSubScreen : OptionsSubScreen
    {
        private MusicVolumeOptionBox _musicVolumeLocalizeableOptionBox;
        private SoundEffectVolumeOptionBox _soundEffectVolumeLocalizeableOptionBox;

        protected override void Start()
        {
            _musicVolumeLocalizeableOptionBox = (MusicVolumeOptionBox)GetComponentInChildren(typeof(MusicVolumeOptionBox));
            _soundEffectVolumeLocalizeableOptionBox = (SoundEffectVolumeOptionBox)GetComponentInChildren(typeof(SoundEffectVolumeOptionBox));
            base.Start();
        }

        protected override void OnApplyButtonClick()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.OptionsScreen);
        }
    }
}
