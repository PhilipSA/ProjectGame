using Assets.Scripts.Engine;
using Assets.Scripts.GUI.GUIElements;
using Assets.Scripts.GUI.OverlayScreens;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;
using UnityEngine.VR.WSA;

namespace Assets.Scripts.GUI
{
    public class GUIHandler : MonoBehaviour
    {
        private TimerDisplay _timerDisplay;
        private ChargeBar _chargeBar;
        private HealthBar _healthBar;
        private FPSDisplay _fpsDisplay;
        private BestTimeDisplay _bestTimeDisplay;
        public VictoryScreen VictoryScreen { get; private set; }
        public DefeatScreen DefeatScreen { get; private set; }
        public PauseScreen PauseMenu { get; private set; }
        public OptionsScreen OptionsScreen { get; private set; }
        public Player PlayerData { get; private set; }

        void Awake()
        {
            _timerDisplay = (TimerDisplay)GetComponentInChildren(typeof(TimerDisplay), true);
            VictoryScreen = (VictoryScreen)GetComponentInChildren(typeof(VictoryScreen), true);
            DefeatScreen = (DefeatScreen)GetComponentInChildren(typeof(DefeatScreen), true);
            PauseMenu = (PauseScreen)GetComponentInChildren(typeof(PauseScreen), true); 
            OptionsScreen = (OptionsScreen)GetComponentInChildren(typeof(OptionsScreen), true);
            PlayerData = FindObjectOfType<Player>();

            _chargeBar = gameObject.AddComponent<ChargeBar>();
            _healthBar = gameObject.AddComponent<HealthBar>();
            _fpsDisplay = gameObject.AddComponent<FPSDisplay>();
            _bestTimeDisplay = gameObject.AddComponent<BestTimeDisplay>();
        }

        public void SetBestTimeDisplay(float time)
        {
            _bestTimeDisplay.SetTime(time);
        }

        void Update()
        {
            _chargeBar.BarDisplay = PlayerData.PlayerBounceLogic.BouncePower;
            _healthBar.BarDisplay = PlayerData.PlayerHitpoints.Hitpoints;
        }

        public void ProcessInputs(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Escape)
            {
                GameEngineHelper.GetCurrentGameEngine().TogglePause();
            }
        }

        public void ToggleOverlayScreen(OverlayScreen overlayScreen)
        {
            overlayScreen.SetVisibility(!overlayScreen.IsVisible);
        }

        public void SetVictoryScreenText(string text)
        {
            VictoryScreen.SetClearingTimeText(text);
        }

        public void StopTimer()
        {
            _timerDisplay.StopTimer();
        }

        public float GetTimerTime()
        {
            return _timerDisplay.StopWatch.TimeSinceStarted;
        }
    }
}
