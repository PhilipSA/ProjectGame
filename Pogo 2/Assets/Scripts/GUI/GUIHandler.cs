using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class GUIHandler : MonoBehaviour
    {
        private TimerDisplay _timerDisplay;
        private ChargeBar _chargeBar;
        private HealthBar _healthBar;
        private VictoryScreen _victoryScreen;
        public Player.Player PlayerData { get; private set; }

        void Start()
        {
            _timerDisplay = gameObject.AddComponent<TimerDisplay>();
            _chargeBar = gameObject.AddComponent<ChargeBar>();
            _healthBar = gameObject.AddComponent<HealthBar>();
            _victoryScreen = gameObject.AddComponent<VictoryScreen>();       
            PlayerData = FindObjectOfType(typeof(Player.Player)) as Player.Player;
        }

        void Update()
        {
            _chargeBar.BarDisplay = PlayerData.PlayerBounceLogic.BouncePower;
        }

        public void DisplayVictoryScreen()
        {
            _victoryScreen.SetVisibility(true);
        }


    }
}
