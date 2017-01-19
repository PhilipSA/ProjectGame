using Assets.Scripts.Interface.Controls.Buttons.Abstraction;
using Assets.Scripts.Menus;
using SmartLocalization;

namespace Assets.Scripts.Interface.Controls.Buttons
{
    public class GraphicOptionsButton : LocalizableButton
    {
        public override void OnClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.GraphicOptionsScreen);
            base.OnClick();
        }

        protected override void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue("GraphicOptionsButton");
            base.Start();
        }
    }
}
