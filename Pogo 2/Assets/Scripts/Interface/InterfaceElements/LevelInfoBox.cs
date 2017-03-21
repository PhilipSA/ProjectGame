using System.Linq;
using Engine.Levels;
using Interface.DisplayFormats;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.InterfaceElements
{
    public class LevelInfoBox : MonoBehaviour
    {
        public LevelInfoBox CreateLevelInfoBox(Level level, RectTransform parent, LevelInfoBox prefab)
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
            tempButton.onClick.AddListener(() => OnClick((int)level.LevelEnum));

            return newPrefabLevelInfoBox;
        }

        void OnClick(int levelIndex)
        {
            LevelHandler.ChangeLevel(levelIndex);
        }
    }
}
