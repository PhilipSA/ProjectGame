using Assets.Scripts.Engine.Levels;
using UnityEngine;

namespace Assets.Scripts.GUI.Buttons
{
    public class MainMenuButton : MonoBehaviour
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel("MainMenu");
        }
    }
}
