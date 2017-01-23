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
        public LevelInfoBox LevelInfoBox { get; private set; }

        void Start()
        {
            _allLevels = LevelHandler.GetAllLevels();

            RectTransform rectTransform = GetComponent<RectTransform>();
            LevelInfoBox = GetComponentInChildren<LevelInfoBox>(true);

            foreach (var level in _allLevels)
            {
                LevelInfoBox.CreateLevelInfoBox(level, rectTransform, LevelInfoBox);              
            }         
        }
    }
}