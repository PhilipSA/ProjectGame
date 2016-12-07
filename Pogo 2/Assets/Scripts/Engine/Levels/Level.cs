using Assets.Scripts.Enums;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine.Levels
{
    public class Level
    {
        public float BestTime { get; private set; }
        public string SceneName { get; private set; }
        public LevelEnum LevelEnum { get; private set; }
        public Scene Scene { get; private set; }

        public Level(float bestTime, string sceneName, LevelEnum levelEnum)
        {
            BestTime = bestTime;
            SceneName = sceneName;
            levelEnum = LevelEnum;
        }

        public void SetLevelScene(Scene scene)
        {
            Scene = scene;
        }
    }
}
