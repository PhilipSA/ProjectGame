using Assets.Scripts.Interface.Controls.Sliders;

namespace Assets.Scripts.Interface.Controls.OptionBox
{
    public class MusicVolumeOptionBox : OptionBox
    {
        private MusicVolumeSlider _musicVolumeSlider;

        protected override void Start()
        {
            _musicVolumeSlider = GetComponentInChildren<MusicVolumeSlider>();
            _musicVolumeSlider.onValueChanged.AddListener(OnValueChanged);
            base.Start();
        }
    }
}
