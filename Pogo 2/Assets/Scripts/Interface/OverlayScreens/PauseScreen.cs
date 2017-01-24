﻿using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class PauseScreen : OverlayScreen
    {
        private ContinueButton _continueButton;
        private RestartLevelButton _restartLevelButton;
        private OptionsButton _optionsButton;
        private ExitButton _exitButton;

        protected override void Start()
        {
            LayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            _continueButton = CreateGameObject.CreateChildGameObject<ContinueButton>(transform).GetComponent<ContinueButton>();
            _restartLevelButton = CreateGameObject.CreateChildGameObject<RestartLevelButton>(transform).GetComponent<RestartLevelButton>();
            _optionsButton = CreateGameObject.CreateChildGameObject<OptionsButton>(transform).GetComponent<OptionsButton>();
            _exitButton = CreateGameObject.CreateChildGameObject<ExitButton>(transform).GetComponent<ExitButton>();
            _exitButton.onClick.AddListener(_exitButton.OnClickInGame);
            base.Start();
        }
    }
}
