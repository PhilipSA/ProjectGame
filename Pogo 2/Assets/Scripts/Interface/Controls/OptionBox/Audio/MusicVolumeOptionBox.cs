using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using Assets.Scripts.Interface.Controls.Sliders;
using Assets.Scripts.MainEngineComponents;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.OptionBox.Audio
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
