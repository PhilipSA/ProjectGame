using Assets.Scripts.Engine;
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
            _timerDisplay = (TimerDisplay)GetComponentInChildren(typeof(TimerDisplay), true);
            VictoryScreen = (VictoryScreen)GetComponentInChildren(typeof(VictoryScreen), true);
            DefeatScreen = (DefeatScreen)GetComponentInChildren(typeof(DefeatScreen), true);
            PauseMenu = (PauseScreen)GetComponentInChildren(typeof(PauseScreen), true); 
            OptionsScreen = (OptionsScreen)GetComponentInChildren(typeof(OptionsScreen), true);
            PlayerData = FindObjectOfType<Player>();
            Debug.Log(VictoryScreen);

            _chargeBar = gameObject.AddComponent<ChargeBar>();
            _healthBar = gameObject.AddComponent<HealthBar>();
            _fpsDisplay = gameObject.AddComponent<FPSDisplay>();
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
