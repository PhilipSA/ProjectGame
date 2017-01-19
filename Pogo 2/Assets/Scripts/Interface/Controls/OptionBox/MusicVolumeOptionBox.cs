using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using Assets.Scripts.Interface.Controls.Sliders;
using UnityEngine;

namespace Assets.Scripts.Interface.Controls.OptionBox
{
    public class MusicVolumeOptionBox : LocalizeableOptionBox
    {
        private MusicVolumeSlider _musicVolumeSlider;

        protected override void Start()
        {
            _musicVolumeSlider = GetComponentInChildren<MusicVolumeSlider>();
            _musicVolumeSlider.onValueChanged.AddListener(OnValueChanged);
            base.Start();
        }

        protected override void OnValueChanged(float newValue)
        {
            //AudioListener.volume = (float)(newValue / 10.0);
            base.OnValueChanged(newValue);
        }
    }
}
