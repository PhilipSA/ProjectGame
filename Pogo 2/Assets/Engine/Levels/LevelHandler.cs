using UnityEngine.SceneManagement;

namespace Assets.Engine.Levels
{
    public static class LevelHandler {

        public static void ChangeLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public static void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
