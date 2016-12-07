using System;
using System.Collections.Generic;
using Assets.Scripts.Engine.FileIO;
using Assets.Scripts.Enums;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine.Levels
{
    public class LevelHandler
    {
        public static void StartNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public static void ChangeLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

        public static void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static Level GetLevelFromScene(int sceneIndex)
        {
            var bestLevelTimeFileHandler = new BestLevelTimeFileHandler("bestTimes.dat");
            return new Level(bestLevelTimeFileHandler.LoadBestTimeForLevel(sceneIndex), ((LevelEnum)sceneIndex).ToString(), ((LevelEnum)sceneIndex));
        }

        public static List<Level> GetAllLevels()
        {
            var bestLevelTimeFileHandler = new BestLevelTimeFileHandler("bestTimes.dat");
            var sceneList = new List<Level>();
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var level = new Level(bestLevelTimeFileHandler.LoadBestTimeForLevel(i), ((LevelEnum)i).ToString(), ((LevelEnum)i));
                sceneList.Add(level);
            }
            return sceneList;
        }
    }
}
