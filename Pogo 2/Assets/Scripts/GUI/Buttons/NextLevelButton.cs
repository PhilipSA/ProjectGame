using Assets.Engine.Levels;
using UnityEngine;

namespace Assets.Scripts.GUI.Buttons
{
    public class NextLevelButton : MonoBehaviour
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel("TestLevel2");
        }
    }
}
