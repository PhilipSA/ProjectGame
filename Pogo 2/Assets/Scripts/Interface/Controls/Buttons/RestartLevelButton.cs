﻿using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class RestartLevelButton : LocalizableButton
    {
        public void OnClick()
        {
            LevelHandler.ReloadCurrentLevel();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("GraphicOptionsButton");
            base.Start();
        }
    }
}
