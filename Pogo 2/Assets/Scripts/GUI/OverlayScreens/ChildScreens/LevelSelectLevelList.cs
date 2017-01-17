using System.Collections.Generic;
using Assets.Scripts.Engine.Levels;
using Assets.Scripts.GUI.GUIElements;
using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens.ChildScreens
{
    public class LevelSelectLevelList : MonoBehaviour
    {
        private List<Level> _allLevels;
        private BestLevelTimeFileHandler _bestLevelTimeFileHandler;
        public GameObject PrefabLevelInfoBox;

        void Start()
        {
            _allLevels = LevelHandler.GetAllLevels();

            RectTransform rectTransform = GetComponent<RectTransform>();

            foreach (var level in _allLevels)
            {
                LevelInfoBox.CreateLevelInfoBox(level, rectTransform, PrefabLevelInfoBox);              
            }         
        }
    }
}