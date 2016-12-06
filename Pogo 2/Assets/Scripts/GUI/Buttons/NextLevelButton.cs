using Assets.Scripts.Engine.Levels;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class NextLevelButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel("TestLevel2");
        }
    }
}
