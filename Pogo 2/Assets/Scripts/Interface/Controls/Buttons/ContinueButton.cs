using Assets.Scripts.Engine;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
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
