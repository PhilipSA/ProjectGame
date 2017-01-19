using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class StartButton : LocalizableButton
    {
        public override void OnClick()
        {
            LevelHandler.ChangeLevel((int)LevelEnum.FlatLand);
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("StartButton");
            base.Start();
        }
    }
}