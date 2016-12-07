using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class MainMenuButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.MainMenu);
        }
    }
}
