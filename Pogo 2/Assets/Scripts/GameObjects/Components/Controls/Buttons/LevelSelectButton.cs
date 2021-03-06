﻿using GameObjects.Components.Controls.Buttons.Abstraction;
using Menus;
using SmartLocalization;

namespace GameObjects.Components.Controls.Buttons
{
    public class LevelSelectButton : LocalizableButton
    {
        public override void OnClick()
        {
            MenuHelper.GetCurrentMenu().ChangeCurrentActiveScreen(MenuHelper.GetCurrentMenu().LevelSelectScreen);
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("LevelSelectButton");
            base.Start();
        }
    }
}
