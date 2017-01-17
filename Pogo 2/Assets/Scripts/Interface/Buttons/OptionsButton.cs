using Assets.Scripts.Interface.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.Interface.Buttons
{
    public class OptionsButton : LocalizableButton
    {       
        public void OnClick()
        {
            MainMenu.ChangeCurrentActiveScreen(MainMenu.OptionsScreenPrefab);
        }

        protected override void Awake()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("OptionsButton");
        }
    }
}
