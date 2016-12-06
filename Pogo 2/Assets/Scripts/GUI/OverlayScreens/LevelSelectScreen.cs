using Assets.Scripts.GUI.OverlayScreens.ChildScreens;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class LevelSelectScreen : OverlayScreen
    {
        private LevelSelectLevelList _levelSelectList;

        public void Init()
        {
            _levelSelectList = GetComponent<LevelSelectLevelList>();
        }
    }
}
