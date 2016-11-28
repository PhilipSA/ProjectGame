using Assets.Scripts.Engine.Levels;
using UnityEngine;

namespace Assets.Scripts.GUI.Buttons
{
    public class RestartLevelButton : MonoBehaviour
    {
        public void OnClick()
        {
            LevelHandler.ReloadCurrentLevel();
        }
    }
}
