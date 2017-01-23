using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction
{
    public abstract class OptionsSubScreen : SubScreen
    {
        protected ApplyButton ApplyButton;

        protected override void Start()
        {
            ApplyButton = CreateGameObject.CreateChildGameObject<ApplyButton>(transform).GetComponent<ApplyButton>();
            ApplyButton.onClick.AddListener(OnApplyButtonClick);
            base.Start();
        }

        protected abstract void OnApplyButtonClick();
    }
}
