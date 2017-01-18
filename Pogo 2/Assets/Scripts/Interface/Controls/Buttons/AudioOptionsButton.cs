using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class AudioOptionsButton : LocalizableButton
    {
        public void OnClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.AudioOptionsScreen);
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("AudioOptionsButton");
            base.Start();
        }
    }
}
