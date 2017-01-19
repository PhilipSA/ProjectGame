using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class NextLevelButton : LocalizableButton
    {
        public override void OnClick()
        {
            LevelHandler.StartNextLevel();
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("NextLevelButton");
            base.Start();
        }
    }
}
