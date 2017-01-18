using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction
{
    public abstract class SubScreen : OverlayScreen
    {
        protected Button BackButton { get; set; }

        protected virtual void SetBackButtonListener(UnityAction function)
        {
            BackButton.onClick.AddListener(function);
        }

        protected abstract void OnBackButtonClick();
    }
}
