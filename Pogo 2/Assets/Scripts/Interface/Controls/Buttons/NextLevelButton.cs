﻿using Assets.Scripts.Engine.Levels;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class NextLevelButton : Button
    {
        public void OnClick()
        {
            LevelHandler.StartNextLevel();
        }
    }
}