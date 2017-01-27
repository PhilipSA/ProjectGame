using Assets.Scripts.Engine.Levels;
using Assets.Scripts.GameObjects.Components.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.GameObjects.Components.Controls.Buttons
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
