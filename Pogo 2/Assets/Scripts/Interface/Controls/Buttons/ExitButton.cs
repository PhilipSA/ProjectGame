using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;
using UnityEngine;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class ExitButton : LocalizableButton
    {
        public override void OnClick()
        {
            Application.Quit();
            base.OnClick();
        }

        public void OnClickInGame()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.MainMenu);
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("ExitButton");
            base.Start();
        }
    }
}
