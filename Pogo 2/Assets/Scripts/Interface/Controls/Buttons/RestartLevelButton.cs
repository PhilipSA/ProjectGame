using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class RestartLevelButton : LocalizableButton
    {
        public override void OnClick()
        {
            LevelHandler.ReloadCurrentLevel();
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("RestartLevelButton");
            base.Start();
        }
    }
}
