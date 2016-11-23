using System.Linq;
using Assets.Engine;
using Assets.Scripts.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class PauseScreen : OverlayScreen
    {
        private Button _continueButton;
        private Button _optionsButton;
        private Button _exitButton;

        protected override void Start()
        {
            _canvas = GameObject.Find("PauseScreen");
            _canvas.SetActive(false);
            InitButtons();
        }

        private void InitButtons()
        {
            _continueButton = _canvas.GetComponentsInChildren<Button>().First(x => x.name == "ContinueButton");
            _continueButton.onClick.AddListener(Continue);

            _optionsButton = _canvas.GetComponentsInChildren<Button>().First(x => x.name == "OptionsButton");
            _optionsButton.onClick.AddListener(Options);

            _exitButton = _canvas.GetComponentsInChildren<Button>().First(x => x.name == "ExitButton");
            _exitButton.onClick.AddListener(Exit);
        }

        public void Continue()
        {
            GameEngineHelper.GetCurrentGameEngine().Pause();
        }

        public void Options()
        {
            GameEngineHelper.GetCurrentGameEngine().Pause();
        }

        public void Exit()
        {
            LevelHandler.ChangeLevel("MainMenu");
        }
    }
}
