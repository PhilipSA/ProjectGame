using Assets.Scripts.Engine.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.GUIElements
{
    public class LevelInfoBox : MonoBehaviour
    {
        public static GameObject CreateLevelInfoBox(string levelName, RectTransform parent, GameObject prefab)
        {
            var newPrefabLevelInfoBox = Instantiate(prefab);
            newPrefabLevelInfoBox.transform.SetParent(parent, false);
            newPrefabLevelInfoBox.transform.localScale = new Vector3(1, 1, 1);
            newPrefabLevelInfoBox.name = levelName;

            var tempText = newPrefabLevelInfoBox.GetComponentInChildren<Text>();
            tempText.text = levelName; 

            var tempButton = newPrefabLevelInfoBox.GetComponentInChildren<Button>();
            tempButton.onClick.AddListener(() => OnClick(levelName));

            return newPrefabLevelInfoBox;
        }

        static void OnClick(string levelName)
        {
            LevelHandler.ChangeLevel(levelName);
        }
    }
}
