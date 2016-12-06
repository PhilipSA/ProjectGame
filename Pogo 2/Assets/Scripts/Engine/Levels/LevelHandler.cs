using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Engine.FileIO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine.Levels
{
    public class LevelHandler
    {
        public static void ChangeLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public static void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static Level GetLevelFromScene(string sceneName)
        {
            var bestLevelTimeFileHandler = new BestLevelTimeFileHandler("bestTimes.dat");
            return new Level(bestLevelTimeFileHandler.LoadBestTimeForLevel(sceneName), sceneName);
        }

        public static List<string> GetAllLevelScenes()
        {
            var unityFileHandler = new UnityFileHandler("/Scenes");
            return unityFileHandler.GetAllLevelScenes();
        }

        public static List<Level> GetAllLevels()
        {
            var sceneList = GetAllLevelScenes();
            var allLevels = new List<Level>();
            foreach (var scene in sceneList)
            {
                var level = GetLevelFromScene(scene);
                allLevels.Add(level);
            }
            return allLevels;
        }
    }
}
