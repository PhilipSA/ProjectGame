using Assets.Scripts.Engine.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class ExitButton : Button
    {
        public void OnClick()
        {
            Application.Quit();
        }

        public void OnClickInGame()
        {
            LevelHandler.ChangeLevel("MainMenu");
        }
    }
}
