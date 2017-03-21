﻿using GameObjects.Components.Controls.Buttons.Abstraction;
using Menus;
using SmartLocalization;

namespace GameObjects.Components.Controls.Buttons
{
    public class AudioOptionsButton : LocalizableButton
    {
        public override void OnClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.AudioOptionsSubScreen);
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("AudioOptionsButton");
            base.Start();
        }
    }
}
