﻿using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using Assets.Scripts.Interface.Controls.Sliders;
using Assets.Scripts.MainEngineComponents;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.OptionBox.Audio
{
    public class SoundEffectVolumeOptionBox : LocalizeableOptionBox
    {
        private SoundEffectSlider _soundEffectSlider;

        protected override void Start()
        {
            _soundEffectSlider = CreateGameObject.CreateChildGameObject<SoundEffectSlider>(transform).GetComponent<SoundEffectSlider>();
            _soundEffectSlider.onValueChanged.AddListener(OnValueChanged);
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
