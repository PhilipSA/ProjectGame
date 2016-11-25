using Assets.Scripts.GUI.GUIElements;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Engine
{
    public class GameEngine : MonoBehaviour
    {
        public GUIHandler GuiHandler;
        public Player Player;
        public InputHandler InputHandler;
        public bool Paused { get; private set; }

        void Start()
        {
            Time.timeScale = 1;
            GuiHandler = gameObject.AddComponent<GUIHandler>();
            Player = GameObject.Find("Player").AddComponent(typeof(Player)) as Player;
            InputHandler = gameObject.AddComponent<InputHandler>();
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
