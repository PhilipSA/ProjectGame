using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class GUIHandler : MonoBehaviour
    {
        private TimerDisplay _timerDisplay;
        private ChargeBar _chargeBar;
        private HealthBar _healthBar;
        private VictoryScreen _victoryScreen;
        private DefeatScreen _defeatScreen;
        private FPSDisplay _fpsDisplay;
        private PauseMenu _pauseMenu;
        public Player.Player PlayerData { get; private set; }

        void Start()
        {
            _timerDisplay = gameObject.AddComponent<TimerDisplay>();
            _chargeBar = gameObject.AddComponent<ChargeBar>();
            _healthBar = gameObject.AddComponent<HealthBar>();
            _victoryScreen = gameObject.AddComponent<VictoryScreen>();
            _defeatScreen = gameObject.AddComponent<DefeatScreen>();
            _fpsDisplay = gameObject.AddComponent<FPSDisplay>();
            _pauseMenu = gameObject.AddComponent<PauseMenu>();
            PlayerData = FindObjectOfType(typeof(Player.Player)) as Player.Player;
        }

        void Update()
        {
            _chargeBar.BarDisplay = PlayerData.PlayerBounceLogic.BouncePower;
            _healthBar.BarDisplay = PlayerData._playerHitpoints.Hitpoints;
        }

        public void DisplayVictoryScreen()
        {
            _victoryScreen.SetVisibility(true);
        }


        public void DisplayPauseMenu()
        {
            _pauseMenu.SetVisibility(true);
        }
    }
}
