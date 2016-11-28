using Assets.Engine;
using Assets.Scripts.GUI.GUIElements;
using Assets.Scripts.GUI.OverlayScreens;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class GUIHandler : MonoBehaviour
    {
        private TimerDisplay _timerDisplay;
        private ChargeBar _chargeBar;
        private HealthBar _healthBar;
        public VictoryScreen VictoryScreen { get; private set; }
        public DefeatScreen DefeatScreen { get; private set; }
        private FPSDisplay _fpsDisplay;
        public PauseScreen PauseMenu { get; private set; }
        public OptionsScreen OptionsScreen { get; private set; }
        public Player PlayerData { get; private set; }

        void Start()
        {
            _timerDisplay = gameObject.AddComponent<TimerDisplay>();
            _chargeBar = gameObject.AddComponent<ChargeBar>();
            _healthBar = gameObject.AddComponent<HealthBar>();
            VictoryScreen = gameObject.AddComponent<VictoryScreen>();
            DefeatScreen = gameObject.AddComponent<DefeatScreen>();
            _fpsDisplay = gameObject.AddComponent<FPSDisplay>();
            PauseMenu = gameObject.AddComponent<PauseScreen>();
            OptionsScreen = gameObject.AddComponent<OptionsScreen>();
            PlayerData = FindObjectOfType(typeof(Player)) as Player;
        }

        void Update()
        {
            _chargeBar.BarDisplay = PlayerData.PlayerBounceLogic.BouncePower;
            _healthBar.BarDisplay = PlayerData._playerHitpoints.Hitpoints;
        }

        public void ProcessInputs(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Escape)
            {
                GameEngineHelper.GetCurrentGameEngine().Pause();
            }
        }

        public void ToggleOverlayScreen(OverlayScreen overlayScreen)
        {
            overlayScreen.SetVisibility(!overlayScreen.IsVisible);
        }

        public void StopTimer()
        {
            _timerDisplay.StopTimer();
        }
    }
}
