using GameObjects.Components.Controls.OptionBox.Abstraction;
using GameObjects.Components.Controls.Sliders;
using MainEngineComponents;
using SmartLocalization;

namespace GameObjects.Components.Controls.OptionBox.Audio
{
    public class SoundEffectVolumeOptionBox : LocalizeableOptionBox
    {
        private SoundEffectSlider _soundEffectSlider;

        protected override void Start()
        {
            _soundEffectSlider = CreateGameObject.CreateChildGameObject<SoundEffectSlider>(transform).GetComponent<SoundEffectSlider>();
            _soundEffectSlider.Slider.onValueChanged.AddListener(OnValueChanged);
            DisplayText = LanguageManager.Instance.GetTextValue("SoundEffects");
            base.Start();
        }

        protected override void OnValueChanged(float newValue)
        {
            MainEngine.GetMainEngine.AudioMixerLevels.SetSfxLevel(newValue);
            base.OnValueChanged(newValue);
        }
    }
}
