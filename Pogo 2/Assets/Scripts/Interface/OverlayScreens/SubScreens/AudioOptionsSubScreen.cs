using Assets.Scripts.Interface.Controls.OptionBox;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class AudioOptionsSubScreen : OptionsSubScreen
    {
        private MusicVolumeOptionBox _musicVolumeOptionBox;
            
        void Awake()
        {
            _musicVolumeOptionBox = (MusicVolumeOptionBox)GetComponentInChildren(typeof(MusicVolumeOptionBox));
        }

        protected override void OnBackButtonClick()
        {
            throw new System.NotImplementedException();
        }
    }
}
