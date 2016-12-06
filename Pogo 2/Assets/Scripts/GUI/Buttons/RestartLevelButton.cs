using Assets.Scripts.Engine.Levels;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class RestartLevelButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ReloadCurrentLevel();
        }
    }
}
