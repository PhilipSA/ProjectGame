using Assets.Scripts.Engine;
using Assets.Scripts.GameObjects.Components.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.GameObjects.Components.Controls.Buttons
{
    public class ContinueButton : LocalizableButton
    {
        public override void OnClick()
        {
            GameEngineHelper.GetCurrentGameEngine().TogglePause();
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("ContinueButton");
            base.Start();
        }
    }
}
