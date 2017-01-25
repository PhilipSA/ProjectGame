using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.OptionBox.Audio;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class AudioOptionsSubScreen : OptionsSubScreen
    {
        private MusicVolumeOptionBox _musicVolumeLocalizeableOptionBox;
        private SoundEffectVolumeOptionBox _soundEffectVolumeLocalizeableOptionBox;

        protected override void Awake()
        {
            _musicVolumeLocalizeableOptionBox = CreateGameObject.CreateChildGameObject<MusicVolumeOptionBox>(transform).GetComponent<MusicVolumeOptionBox>();
            _soundEffectVolumeLocalizeableOptionBox = CreateGameObject.CreateChildGameObject<SoundEffectVolumeOptionBox>(transform).GetComponent<SoundEffectVolumeOptionBox>();
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
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
