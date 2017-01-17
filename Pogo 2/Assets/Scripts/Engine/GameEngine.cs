using System;
using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Enums;
using Assets.Scripts.GUI;
using Assets.Scripts.GUI.DisplayFormats;
using Assets.Scripts.InteractingObjects.Player;
using SmartLocalization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine
{
    public class GameEngine : MonoBehaviour
    {
        public GuiHandler GuiHandler { get; private set; }
        public Player Player { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public GameEvents GameEvents { get; private set; }
        public bool Paused { get; private set; }
        public BestLevelTimeFileHandler BestLevelTime { get; private set; }
        public Level Level { get; private set; }

        void Start()
        {
            Time.timeScale = 1;
            GuiHandler = (GuiHandler)GetComponentInChildren(typeof(GuiHandler));
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

            Debug.Log(LanguageManager.Instance.GetTextValue("NoNewRecord"));
        }

        void Update()
        {
            
        }

        void InputSubscriptions()
        {
            InputHandler.PlayerSubscribe(Player);
            InputHandler.GuiSubscribe(GuiHandler);
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
                GuiHandler.SetVictoryScreenText(String.Format("{0} {1}", "New record:" , TimeFormatter.GetTimeInMmssffFormat(GuiHandler.GetTimerTime())));
                GuiHandler.SetBestTimeDisplay(GuiHandler.GetTimerTime());
            }
            else
            {
                GuiHandler.SetVictoryScreenText(LanguageManager.Instance.GetTextValue("NoNewRecord"));
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
