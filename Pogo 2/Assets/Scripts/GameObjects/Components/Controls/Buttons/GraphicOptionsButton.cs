using GameObjects.Components.Controls.Buttons.Abstraction;
using Menus;
using SmartLocalization;

namespace GameObjects.Components.Controls.Buttons
{
    public class GraphicOptionsButton : LocalizableButton
    {
        public override void OnClick()
        {
            MenuHelper.GetCurrentMenu().ChangeCurrentActiveScreen(MenuHelper.GetCurrentMenu().GraphicOptionsSubScreen);
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("GraphicOptionsButton");
            base.Start();
        }
    }
}
