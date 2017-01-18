using System.Collections.Generic;
using Assets.Scripts.Engine.Levels;
using Assets.Scripts.Interface.InterfaceElements;
using UnityEngine;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
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