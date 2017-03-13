using Assets.Scripts.Engine;
using Assets.Scripts.GameObjects;
using Assets.Scripts.InteractingObjects.Player;
using Assets.Scripts.Interface.InterfaceElements;
using Assets.Scripts.Interface.OverlayScreens;
using Assets.Scripts.Interface.OverlayScreens.Abstraction;
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
        private FloatingTextDisplay _floatingTextDisplay;
        public VictoryScreen VictoryScreen { get; private set; }
        public DefeatScreen DefeatScreen { get; private set; }
        public PauseMenu PauseMenu { get; private set; }
        public Player PlayerData { get; private set; }

        void Awake()
        {
            _timerDisplay = CreateGameObject.CreateChildGameObject<TimerDisplay>(transform).GetComponent<TimerDisplay>();
            VictoryScreen = CreateGameObject.CreateChildGameObject<VictoryScreen>(transform).GetComponent<VictoryScreen>();
            DefeatScreen = CreateGameObject.CreateChildGameObject<DefeatScreen>(transform).GetComponent<DefeatScreen>();
            PauseMenu = CreateGameObject.CreateChildGameObject<PauseMenu>(transform).GetComponent<PauseMenu>();
            PlayerData = FindObjectOfType<Player>();

            _floatingTextDisplay = CreateGameObject.CreateChildGameObject<FloatingTextDisplay>(transform).GetComponent<FloatingTextDisplay>();
            _chargeBar = CreateGameObject.CreateChildGameObject<ChargeBar>(transform).GetComponent<ChargeBar>();
            _healthBar = CreateGameObject.CreateChildGameObject<HealthBar>(transform).GetComponent<HealthBar>();
            _bestTimeDisplay = CreateGameObject.CreateChildGameObject<BestTimeDisplay>(transform).GetComponent<BestTimeDisplay>();
        }

        public void SetBestTimeDisplay(float time)
        {
            _bestTimeDisplay.SetTime(time);
        }

        public void DisplayFloatingText(string text)
        {
            _floatingTextDisplay.SetAndEnable(text);
        }

        void Update()
        {
            _chargeBar.BarDisplay = Mathf.FloorToInt(PlayerData.PlayerBounceLogic.BouncePower);
            _healthBar.BarDisplay = PlayerData.PlayerHitpoints.Hitpoints;
        }

        public void PauseInvoked()
        {
            GameEngineHelper.GetCurrentGameEngine().TogglePause();
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
