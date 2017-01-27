using Assets.Scripts.GameObjects.Components.Controls.OptionBox.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Sliders;
using Assets.Scripts.MainEngineComponents;
using SmartLocalization;

namespace Assets.Scripts.GameObjects.Components.Controls.OptionBox.Audio
{
    public class MusicVolumeOptionBox : LocalizeableOptionBox
    {
        private MusicVolumeSlider _musicVolumeSlider;

        protected override void Start()
        {
            _musicVolumeSlider = CreateGameObject.CreateChildGameObject<MusicVolumeSlider>(transform).GetComponent<MusicVolumeSlider>();
            _musicVolumeSlider.onValueChanged.AddListener(OnValueChanged);

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
