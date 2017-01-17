using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Buttons
{
    public class StartButton : Button
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.FlatLand);
        }
    }
}