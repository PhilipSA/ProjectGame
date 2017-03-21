using Engine.Levels;
using Enums.Levels;
using GameObjects.Components.Controls.Buttons.Abstraction;
using SmartLocalization;
using UnityEngine;

namespace GameObjects.Components.Controls.Buttons
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
