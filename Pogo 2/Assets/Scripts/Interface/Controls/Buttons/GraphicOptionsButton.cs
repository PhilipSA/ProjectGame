﻿using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class GraphicOptionsButton : LocalizableButton
    {
        public void OnClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.GraphicOptionsScreen);
        }

        protected override void Awake()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("GraphicOptionsButton");
        }
    }
}
