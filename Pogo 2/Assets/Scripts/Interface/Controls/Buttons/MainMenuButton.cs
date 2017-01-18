using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class MainMenuButton : LocalizableButton
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.MainMenu);
        }

        protected override void Awake()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("MainMenuButton");
        }
    }
}
