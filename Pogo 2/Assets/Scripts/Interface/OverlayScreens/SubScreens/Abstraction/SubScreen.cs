using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.OverlayScreens.Abstraction;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction
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
