using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Engine.Levels;
using Assets.Scripts.GUI.GUIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GUI.OverlayScreens.ChildScreens
{
    public class LevelSelectLevelList : MonoBehaviour
    {
        private List<Level> allLevels;
        private BestLevelTimeFileHandler bestLevelTimeFileHandler;
        public GameObject prefabLevelInfoBox;

        void Start()
        {
            allLevels = LevelHandler.GetAllLevels();

            RectTransform rectTransform = GetComponent<RectTransform>();

            foreach (var level in allLevels)
            {
                LevelInfoBox.CreateLevelInfoBox(level, rectTransform, prefabLevelInfoBox);              
            }         
        }
    }
}