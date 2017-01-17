using Assets.Scripts.Interface.OverlayScreens.ChildScreens;

namespace Assets.Scripts.Interface.OverlayScreens
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
