using Assets.Scripts.GUI.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.GUI.Buttons
{
    public class OptionsButton : LocalizableButton
    {       
        public void OnClick()
        {
            MainMenu.ChangeCurrentActiveScreen(MainMenu.OptionsScreen);
        }

        protected override void Awake()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("OptionsButton");
        }
    }
}
