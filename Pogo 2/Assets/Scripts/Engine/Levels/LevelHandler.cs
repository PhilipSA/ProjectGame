using System.Collections.Generic;
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

        public static Level GetLevelFromScene(Scene scene, BestLevelTimeFileHandler bestLevelTimeFileHandler)
        {
            return new Level(bestLevelTimeFileHandler.LoadBestTimeForLevel(scene.name), scene);
        }

        public static List<Scene> GetAllLevelScenes()
        {
            var sceneList = new List<Scene>();
            for (int i = 1; i < SceneManager.sceneCount; i++)
            {
                sceneList.Add(SceneManager.GetSceneAt(i));
            }
            return sceneList;
        }

        public static List<Level> GetAllLevels(BestLevelTimeFileHandler bestLevelTimeFileHandler)
        {
            var sceneList = GetAllLevelScenes();
            var allLevels = new List<Level>();
            foreach (var scene in sceneList)
            {
                var level = GetLevelFromScene(scene, bestLevelTimeFileHandler);
                allLevels.Add(level);
            }
            return allLevels;
        }
    }
}
