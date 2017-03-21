using GameObjects;
using GameObjects.Components.Controls.Buttons;

namespace Interface.OverlayScreens.SubScreens.Abstraction
{
    public abstract class OptionsSubScreen : SubScreen
    {
        protected ApplyButton ApplyButton;

        protected override void Awake()
        {
            ApplyButton = CreateGameObject.CreateChildGameObject<ApplyButton>(transform).GetComponent<ApplyButton>();
            ApplyButton.onClick.AddListener(OnApplyButtonClick);
            base.Awake();
        }

        protected abstract void OnApplyButtonClick();
    }
}
