using Engine.Levels;
using GameObjects.Components.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace GameObjects.Components.Controls.Buttons
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
