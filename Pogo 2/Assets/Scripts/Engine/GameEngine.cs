﻿using Assets.Scripts.Engine.Levels;
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
        public BestLevelTime BestLevelTime { get; private set; }
        public Level Level { get; private set; }

        void Start()
        {
            Time.timeScale = 1;
            GuiHandler = (GUIHandler)GetComponentInChildren(typeof(GUIHandler));
            Player = (Player)GetComponentInChildren(typeof(Player));
            InputHandler = (InputHandler)GetComponentInChildren(typeof(InputHandler));

            BestLevelTime = new BestLevelTime("bestTimes.dat");
            Level = new Level(BestLevelTime.LoadBestTimeForLevel(SceneManager.GetActiveScene().name), SceneManager.GetActiveScene());
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
                BestLevelTime.SaveBestTimeForLevel(SceneManager.GetActiveScene().name, GuiHandler.GetTimerTime());
                GuiHandler.SetVictoryScreenText(GuiHandler.GetTimerTime().ToString());
            }
            else
            {
                GuiHandler.SetVictoryScreenText("U suck lul");
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
