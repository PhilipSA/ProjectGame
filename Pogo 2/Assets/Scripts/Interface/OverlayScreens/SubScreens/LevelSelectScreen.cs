using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class LevelSelectScreen : SubScreen
    {
        private LevelSelectLevelList _levelSelectList;

        public void Init()
        {
            _levelSelectList = GetComponent<LevelSelectLevelList>();
        }

        protected override void OnBackButtonClick()
        {
            throw new System.NotImplementedException();
        }
    }
}
