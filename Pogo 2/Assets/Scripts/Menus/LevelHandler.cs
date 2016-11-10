using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menus
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
