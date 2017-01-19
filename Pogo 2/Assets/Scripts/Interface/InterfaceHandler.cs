using Assets.Scripts.Engine;
using Assets.Scripts.InteractingObjects.Player;
using Assets.Scripts.Interface.InterfaceElements;
using Assets.Scripts.Interface.OverlayScreens;
using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class InterfaceHandler : MonoBehaviour
    {
        private TimerDisplay _timerDisplay;
        private ChargeBar _chargeBar;
        private HealthBar _healthBar;
        private BestTimeDisplay _bestTimeDisplay;
        public VictoryScreen VictoryScreen { get; private set; }
        public DefeatScreen DefeatScreen { get; private set; }
        public PauseMenu PauseMenu { get; private set; }
        public Player PlayerData { get; private set; }

        void Awake()
        {
            _timerDisplay = (TimerDisplay)GetComponentInChildren(typeof(TimerDisplay), true);
            VictoryScreen = (VictoryScreen)GetComponentInChildren(typeof(VictoryScreen), true);
            DefeatScreen = (DefeatScreen)GetComponentInChildren(typeof(DefeatScreen), true);
            PauseMenu = (PauseMenu)GetComponentInChildren(typeof(PauseMenu), true); 
            PlayerData = FindObjectOfType<Player>();

            _chargeBar = GetComponentInChildren<ChargeBar>();
            _healthBar = GetComponentInChildren<HealthBar>();
            _bestTimeDisplay = GetComponentInChildren<BestTimeDisplay>();
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
