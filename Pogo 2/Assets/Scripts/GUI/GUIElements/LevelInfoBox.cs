using System.Linq;
using Assets.Scripts.Engine.Levels;
using Assets.Scripts.GUI.DisplayFormats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.GUIElements
{
    public class LevelInfoBox : MonoBehaviour
    {
        public static GameObject CreateLevelInfoBox(Level level, RectTransform parent, GameObject prefab)
        {
            var newPrefabLevelInfoBox = Instantiate(prefab);
            newPrefabLevelInfoBox.transform.SetParent(parent, false);
            newPrefabLevelInfoBox.transform.localScale = new Vector3(1, 1, 1);
            newPrefabLevelInfoBox.name = level.SceneName;

            var levelNameText = newPrefabLevelInfoBox.GetComponentsInChildren<Text>().First();
            levelNameText.text = level.SceneName;

            var bestTimeText = newPrefabLevelInfoBox.GetComponentsInChildren<Text>().Last();
            bestTimeText.text = "Best time: " + TimeFormatter.GetTimeInMmssffFormat(level.BestTime);

            var tempButton = newPrefabLevelInfoBox.GetComponentInChildren<Button>();
            tempButton.onClick.AddListener(() => OnClick(level.SceneName));

            return newPrefabLevelInfoBox;
        }

        static void OnClick(string levelName)
        {
            LevelHandler.ChangeLevel(levelName);
        }
    }
}
