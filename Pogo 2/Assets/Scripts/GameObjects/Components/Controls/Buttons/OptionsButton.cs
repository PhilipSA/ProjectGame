using GameObjects.Components.Controls.Buttons.Abstraction;
using Menus;
using SmartLocalization;

namespace GameObjects.Components.Controls.Buttons
{
    public class OptionsButton : LocalizableButton
    {       
        public override void OnClick()
        {
            MenuHelper.GetCurrentMenu().ChangeCurrentActiveScreen(MenuHelper.GetCurrentMenu().OptionsScreen);
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("OptionsButton");
            base.Start();
        }
    }
}
