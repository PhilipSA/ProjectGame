using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class StartButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.FlatLand);
        }
    }
}