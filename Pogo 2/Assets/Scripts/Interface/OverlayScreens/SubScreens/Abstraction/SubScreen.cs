using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using UnityEngine;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction
{
    public abstract class SubScreen : OverlayScreen
    {
        protected BackButton BackButton { get; set; }

        protected override void Start()
        {
            BackButton = CreateGameObject.CreateChildGameObject<BackButton>(transform).GetComponent<BackButton>();
            BackButton.onClick.AddListener(OnBackButtonClick);
            base.Start();
        }

        protected abstract void OnBackButtonClick();
    }
}
