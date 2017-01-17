using Assets.Scripts.Engine.Levels;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Buttons
{
    public class RestartLevelButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ReloadCurrentLevel();
        }
    }
}
