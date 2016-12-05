using Assets.Scripts.GUI.OverlayScreens.ChildScreens;
using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class LevelSelectScreen : OverlayScreen
    {
        private LevelSelectLevelList LevelSelectList;

        void Awake()
        {
            LevelSelectList = GetComponent<LevelSelectLevelList>();
        }

        void OnGUI()
        {
            
        }
    }
}
