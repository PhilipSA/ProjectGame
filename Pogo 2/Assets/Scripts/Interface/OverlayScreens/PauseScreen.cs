using Assets.Scripts.GameObjects;
using Assets.Scripts.GameObjects.Components.Controls.Buttons;
using Assets.Scripts.Interface.OverlayScreens.Abstraction;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class PauseScreen : OverlayScreen
    {
        private ContinueButton _continueButton;
        private RestartLevelButton _restartLevelButton;
        private OptionsButton _optionsButton;
        private ExitButton _exitButton;

        protected override void Awake()
        {
            _continueButton = CreateGameObject.CreateChildGameObject<ContinueButton>(transform).GetComponent<ContinueButton>();
            _restartLevelButton = CreateGameObject.CreateChildGameObject<RestartLevelButton>(transform).GetComponent<RestartLevelButton>();
            _optionsButton = CreateGameObject.CreateChildGameObject<OptionsButton>(transform).GetComponent<OptionsButton>();
            _exitButton = CreateGameObject.CreateChildGameObject<ExitButton>(transform).GetComponent<ExitButton>();
            _exitButton.onClick.AddListener(_exitButton.OnClickInGame);
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
        }
    }
}
