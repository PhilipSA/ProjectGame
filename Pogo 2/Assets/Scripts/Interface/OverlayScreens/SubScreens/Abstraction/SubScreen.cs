using GameObjects;
using GameObjects.Components.Controls.Buttons;
using Interface.OverlayScreens.Abstraction;

namespace Interface.OverlayScreens.SubScreens.Abstraction
{
    public abstract class SubScreen : OverlayScreen
    {
        protected BackButton BackButton { get; set; }

        protected override void Awake()
        {
            BackButton = CreateGameObject.CreateChildGameObject<BackButton>(transform).GetComponent<BackButton>();
            BackButton.onClick.AddListener(OnBackButtonClick);
            base.Awake();
        }

        protected abstract void OnBackButtonClick();
    }
}
