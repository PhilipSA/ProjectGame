using UnityEngine.SceneManagement;

namespace Assets.Scripts.Engine.Levels
{
    public class Level
    {
        public float BestTime { get; private set; }
        public Scene Scene { get; private set; }

        public Level(float bestTime, Scene scene)
        {
            BestTime = bestTime;
            Scene = scene;
        }
    }
}
