using Assets.Scripts.Engine;
using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class ContinueButton : LocalizableButton
    {
        public void OnClick()
        {
            GameEngineHelper.GetCurrentGameEngine().TogglePause();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("ContinueButton");
            base.Start();
        }
    }
}
