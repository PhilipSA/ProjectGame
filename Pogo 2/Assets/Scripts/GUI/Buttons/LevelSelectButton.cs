using Assets.Scripts.GUI.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.GUI.Buttons
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
