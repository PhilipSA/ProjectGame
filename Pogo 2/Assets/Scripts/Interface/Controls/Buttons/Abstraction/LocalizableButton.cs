﻿using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Buttons.Abstraction
{
    public class LocalizableButton : Button
    {
        public string DisplayText;

        protected override void Start()
        {
            GetComponentInChildren<Text>().text = DisplayText;
        }
    }
}