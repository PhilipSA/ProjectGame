using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine.Levels
{
    public class Level
    {
        public float BestTime { get; private set; }
        public string SceneName { get; private set; }
        public Scene Scene { get; private set; }

        public Level(float bestTime, string sceneName)
        {
            BestTime = bestTime;
            SceneName = sceneName;
        }

        public void SetLevelScene(Scene scene)
        {
            Scene = scene;
        }
    }
}
