﻿using System;
using Cameras;
using Engine.Events;
using Engine.Input;
using Engine.Levels;
using Enums.Levels;
using GameObjects;
using GameObjects.InteractingObjects.Player;
using Interface;
using Interface.DisplayFormats;
using SmartLocalization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class GameEngine : MonoBehaviour
    {
        public InterfaceHandler InterfaceHandler { get; private set; }
        public Player Player { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public GameEvents GameEvents { get; private set; }
        public bool Paused { get; private set; }
        public BestLevelTimeFileHandler BestLevelTime { get; private set; }
        public Level Level { get; private set; }
        public MainCamera MainCamera;

        void Awake()
        {
            Time.timeScale = 1;
            InterfaceHandler = CreateGameObject.CreateChildGameObject<InterfaceHandler>(transform).GetComponent<InterfaceHandler>();
            Player = (Player)GetComponentInChildren(typeof(Player));
            InputHandler = CreateGameObject.CreateChildGameObject<InputHandler>(transform).GetComponent<InputHandler>();

            BestLevelTime = new BestLevelTimeFileHandler("bestTimes.dat");
            Level = new Level(BestLevelTime.LoadBestTimeForLevel(SceneManager.GetActiveScene().buildIndex), SceneManager.GetActiveScene().name, 
                (LevelEnum)SceneManager.GetActiveScene().buildIndex);
            InterfaceHandler.SetBestTimeDisplay(Level.BestTime);

            MainCamera = GetComponentInChildren<MainCamera>();

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
            InputHandler.PlayerSubscribeToInputs(Player);
            InputHandler.GuiSubscribe(InterfaceHandler);
        }

        public void Victory()
        {
            LevelEnded();
            CheckIfBestTime();
            InterfaceHandler.ToggleOverlayScreen(InterfaceHandler.VictoryScreen);
        }

        public void CheckIfBestTime()
        {
            if (InterfaceHandler.GetTimerTime() < Level.BestTime || Level.BestTime.Equals(0))
            {
                BestLevelTime.SaveBestTimeForLevel(SceneManager.GetActiveScene().buildIndex, InterfaceHandler.GetTimerTime());
                InterfaceHandler.SetVictoryScreenText(String.Format("{0} {1}", "New record:" , TimeFormatter.GetTimeInMmssffFormat(InterfaceHandler.GetTimerTime())));
                InterfaceHandler.SetBestTimeDisplay(InterfaceHandler.GetTimerTime());
            }
            else
            {
                InterfaceHandler.SetVictoryScreenText(LanguageManager.Instance.GetTextValue("NoNewRecord"));
            }
        }

        public void LevelEnded()
        {
            GameEvents.PlayerOnGoalCollision -= Victory;
            InputHandler.ToggleIgnorePlayerInputs(true, Player);
            InterfaceHandler.StopTimer();
            Player.enabled = false;
        }

        public void Defeat()
        {
            LevelEnded();
            InterfaceHandler.ToggleOverlayScreen(InterfaceHandler.DefeatScreen);
        }

        public void TogglePauseMenu(bool toggle)
        {
            InterfaceHandler.PauseMenu.ToggleDisplay(toggle);
        }

        public void TogglePause()
        {
            Paused = !Paused;
            TogglePauseMenu(Paused);
            Time.timeScale = Paused ? 0 : 1;
            Player.enabled = !Paused;
            InputHandler.ToggleIgnorePlayerInputs(Paused, Player);
        }

    }
}
