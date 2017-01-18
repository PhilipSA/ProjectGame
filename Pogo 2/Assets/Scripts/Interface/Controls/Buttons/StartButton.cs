using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class StartButton : LocalizableButton
    {
        public void OnClick()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.FlatLand);
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("StartButton");
            base.Start();
        }
    }
}