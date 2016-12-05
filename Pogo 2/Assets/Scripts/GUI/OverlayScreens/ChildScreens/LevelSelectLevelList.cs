using System.Collections.Generic;
using Assets.Scripts.Engine.Levels;
using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens.ChildScreens
{
    public class LevelSelectLevelList : MonoBehaviour
    {
        private List<Level> allLevels;
        private BestLevelTimeFileHandler bestLevelTimeFileHandler;

        void Start()
        {
            bestLevelTimeFileHandler = new BestLevelTimeFileHandler("bestTimes.dat");
            allLevels = LevelHandler.GetAllLevels(bestLevelTimeFileHandler);
        }

        void OnGUI()
        {         
            int w = Screen.width, h = Screen.height;

            foreach (var level in allLevels)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 12;
                Rect rect = new Rect(0, 0, w, h * 2 / 100);
                string text = level.Scene.name;
                UnityEngine.GUI.Label(rect, text, style);
            }
        }
    }
}