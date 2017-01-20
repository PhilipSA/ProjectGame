using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using Assets.Scripts.Interface.Controls.Sliders;
using Assets.Scripts.MainEngineComponents;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.OptionBox
{
    public class MusicVolumeOptionBox : LocalizeableOptionBox
    {
        private MusicVolumeSlider _musicVolumeSlider;

        protected override void Start()
        {
            _musicVolumeSlider = GetComponentInChildren<MusicVolumeSlider>();
            _musicVolumeSlider.onValueChanged.AddListener(OnValueChanged);
            DisplayText = LanguageManager.Instance.GetTextValue("BackgroundMusic");
            base.Start();
        }

        protected override void OnValueChanged(float newValue)
        {
            MainEngine.AudioMixerLevels.SetMusicLevel(newValue);
            base.OnValueChanged(newValue);
        }
    }
}
