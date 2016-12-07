using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using Assets.Scripts.GUI;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine
{
    public class GameEngine : MonoBehaviour
    {
        public GUIHandler GuiHandler { get; private set; }
        public Player Player { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public GameEvents GameEvents { get; private set; }
        public bool Paused { get; private set; }
        public BestLevelTimeFileHandler BestLevelTime { get; private set; }
        public Level Level { get; private set; }

        void Start()
        {
            Time.timeScale = 1;
            GuiHandler = (GUIHandler)GetComponentInChildren(typeof(GUIHandler));
            Player = (Player)GetComponentInChildren(typeof(Player));
            InputHandler = (InputHandler)GetComponentInChildren(typeof(InputHandler));

            BestLevelTime = new BestLevelTimeFileHandler("bestTimes.dat");
            Level = new Level(BestLevelTime.LoadBestTimeForLevel(SceneManager.GetActiveScene().buildIndex), SceneManager.GetActiveScene().name, 
                (LevelEnum)SceneManager.GetActiveScene().buildIndex);
            GuiHandler.SetBestTimeDisplay(Level.BestTime);

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
            InputHandler.ToggleIgnorePlayerInputs(true, Player);
            GuiHandler.StopTimer();
            CheckIfBestTime();
            GuiHandler.ToggleOverlayScreen(GuiHandler.VictoryScreen);
            Player.enabled = false;
        }

        public void CheckIfBestTime()
        {
            if (GuiHandler.GetTimerTime() < Level.BestTime || Level.BestTime.Equals(0))
            {
                BestLevelTime.SaveBestTimeForLevel(SceneManager.GetActiveScene().buildIndex, GuiHandler.GetTimerTime());
                GuiHandler.SetVictoryScreenText("New record: "+GuiHandler.GetTimerTime().ToString());
                GuiHandler.SetBestTimeDisplay(GuiHandler.GetTimerTime());
            }
            else
            {
                GuiHandler.SetVictoryScreenText("No new record");
            }
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

        public void TogglePause()
        {
            Paused = !Paused;
            TogglePauseMenu();
            Time.timeScale = Paused ? 0 : 1;
            Player.enabled = !Paused;
            InputHandler.ToggleIgnorePlayerInputs(Paused, Player);
        }

    }
}
