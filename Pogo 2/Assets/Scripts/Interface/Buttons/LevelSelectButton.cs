using Assets.Scripts.Interface.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.Interface.Buttons
{
    public class LevelSelectButton : LocalizableButton
    {
        public void OnClick()
        {
            MainMenu.ChangeCurrentActiveScreen(MainMenu.LevelSelectScreen);
        }

        protected override void Awake()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("LevelSelectButton");
        }
    }
}
