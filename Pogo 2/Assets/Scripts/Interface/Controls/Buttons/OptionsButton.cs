using Assets.Scripts.Engine.Audio;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class OptionsButton : LocalizableButton
    {       
        public void OnClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.OptionsScreen);
            AudioHandler.PlayAudio(AudioSource);
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("OptionsButton");
            base.Start();
        }
    }
}
