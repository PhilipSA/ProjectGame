using Assets.Scripts.GUI;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class GameEngine : MonoBehaviour
    {
        public GUIHandler GuiHandler { get; private set; }
        public Player Player { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public GameEvents GameEvents { get; private set; }
        public bool Paused { get; private set; }

        void Start()
        {
            Time.timeScale = 1;
            GuiHandler = (GUIHandler)GetComponentInChildren(typeof(GUIHandler));
            Player = (Player)GetComponentInChildren(typeof(Player));
            InputHandler = (InputHandler)GetComponentInChildren(typeof(InputHandler));
            GameEvents = new GameEvents();
            GameEvents.PlayerOnGoalCollision += Victory;
            InputSubscriptions();
            Paused = false;
        }

        void Update()
        {
            
        }

        void InputSubscriptions()
        {
            InputHandler.PlayerSubscribe(Player);
            InputHandler.GUISubscribe(GuiHandler);
        }

        public void Victory()
        {
            GameEvents.PlayerOnGoalCollision -= Victory;
            GuiHandler.ToggleOverlayScreen(GuiHandler.VictoryScreen);
            InputHandler.ToggleIgnorePlayerInputs(true, Player);
            GuiHandler.StopTimer();
            Player.enabled = false;
        }

        public void Defeat()
        {
            GuiHandler.ToggleOverlayScreen(GuiHandler.DefeatScreen);
            InputHandler.ToggleIgnorePlayerInputs(true, Player);
            GuiHandler.StopTimer();
            Player.enabled = false;
        }

        public void TogglePauseMenu()
        {
            GuiHandler.ToggleOverlayScreen(GuiHandler.PauseMenu);
        }

        public void Pause()
        {
            Paused = !Paused;
            TogglePauseMenu();
            Time.timeScale = Paused ? 0 : 1;
            Player.enabled = !Paused;
            InputHandler.ToggleIgnorePlayerInputs(Paused, Player);
        }

    }
}
