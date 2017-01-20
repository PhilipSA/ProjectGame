using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class ApplyButton : LocalizableButton
    {
        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("ApplyButton");
            onClick.AddListener(OnClick);
            base.Start();
        }

        public override void OnClick()
        {
            base.OnClick();
        }
    }
}
