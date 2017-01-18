using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class ExitButton : Button
    {
        public void OnClick()
        {
            Application.Quit();
        }

        public void OnClickInGame()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.MainMenu);
        }
    }
}
