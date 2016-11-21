using Assets.Scripts.GUI;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Engine
{
    public class GameEngine : MonoBehaviour
    {
        public GUIHandler GuiHandler;
        public Player Player;
        public InputHandler InputHandler;

        void Start()
        {
            GuiHandler = gameObject.AddComponent<GUIHandler>();
            Player = GameObject.Find("Player").AddComponent(typeof(Player)) as Player;
            InputHandler = gameObject.AddComponent<InputHandler>();
            InputSubscriptions();
        }

        void Update()
        {
            
        }

        void InputSubscriptions()
        {
            InputHandler.PlayerSubscribe(Player);
        }

        public void Victory()
        {
            GuiHandler.DisplayVictoryScreen();
        }

        public void DisplayPauseMenu()
        {
            GuiHandler.DisplayPauseMenu();
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

    }
}
