using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using Assets.Scripts.Interface.Controls.Sliders;
using Assets.Scripts.MainEngineComponents;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.OptionBox
{
    public class SoundEffectVolumeOptionBox : LocalizeableOptionBox
    {
        private SoundEffectSlider _soundEffectSlider;

        protected override void Start()
        {
            _soundEffectSlider = GetComponentInChildren<SoundEffectSlider>();
            _soundEffectSlider.onValueChanged.AddListener(OnValueChanged);
            DisplayText = LanguageManager.Instance.GetTextValue("SoundEffects");
            base.Start();
        }

        protected override void OnValueChanged(float newValue)
        {
            MainEngine.AudioMixerLevels.SetSfxLevel(newValue);
            base.OnValueChanged(newValue);
        }
    }
}
