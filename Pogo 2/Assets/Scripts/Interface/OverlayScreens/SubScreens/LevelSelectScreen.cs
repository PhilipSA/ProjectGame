using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class LevelSelectScreen : SubScreen
    {
        private LevelSelectLevelList _levelSelectList;

        protected override void Start()
        {
            _levelSelectList = GetComponent<LevelSelectLevelList>();
            base.Start();
        }

        protected override void OnBackButtonClick()
        {
            throw new System.NotImplementedException();
        }
    }
}
