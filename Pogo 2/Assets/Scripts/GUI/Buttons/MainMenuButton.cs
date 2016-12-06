using Assets.Scripts.Engine.Levels;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class MainMenuButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel("MainMenu");
        }
    }
}
