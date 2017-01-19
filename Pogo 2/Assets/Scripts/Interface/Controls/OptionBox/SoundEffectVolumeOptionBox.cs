using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using Assets.Scripts.Interface.Controls.Sliders;

namespace Assets.Scripts.Interface.Controls.OptionBox
{
    public class SoundEffectVolumeOptionBox : LocalizeableOptionBox
    {
        private SoundEffectSlider _soundEffectSlider;

        protected override void Start()
        {
            _soundEffectSlider = GetComponentInChildren<SoundEffectSlider>();
            base.Start();
        }
    }
}
