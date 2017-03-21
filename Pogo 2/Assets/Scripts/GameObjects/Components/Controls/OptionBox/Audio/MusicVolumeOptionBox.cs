using GameObjects.Components.Controls.OptionBox.Abstraction;
using GameObjects.Components.Controls.Sliders;
using MainEngineComponents;
using SmartLocalization;

namespace GameObjects.Components.Controls.OptionBox.Audio
{
    public class MusicVolumeOptionBox : LocalizeableOptionBox
    {
        private MusicVolumeSlider _musicVolumeSlider;

        protected override void Start()
        {
            _musicVolumeSlider = CreateGameObject.CreateChildGameObject<MusicVolumeSlider>(transform).GetComponent<MusicVolumeSlider>();
            _musicVolumeSlider.Slider.onValueChanged.AddListener(OnValueChanged);

            DisplayText = LanguageManager.Instance.GetTextValue("BackgroundMusic");
            base.Start();
        }

        protected override void OnValueChanged(float newValue)
        {
            MainEngine.GetMainEngine.AudioMixerLevels.SetMusicLevel(newValue);
            base.OnValueChanged(newValue);
        }
    }
}
