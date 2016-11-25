using Assets.Engine.Levels;
using Assets.Scripts.Menus;
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
