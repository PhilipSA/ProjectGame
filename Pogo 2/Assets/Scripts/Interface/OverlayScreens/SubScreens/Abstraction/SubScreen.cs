using Assets.Scripts.Interface.Controls.Buttons;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction
{
    public abstract class SubScreen : OverlayScreen
    {
        protected Button BackButton { get; set; }

        protected override void Start()
        {
            BackButton = GetComponentInChildren<BackButton>();
            BackButton.onClick.AddListener(OnBackButtonClick);
            base.Start();
        }

        protected abstract void OnBackButtonClick();
    }
}
