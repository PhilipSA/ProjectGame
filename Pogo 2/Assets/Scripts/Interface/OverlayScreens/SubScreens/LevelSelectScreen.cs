using GameObjects;
using Interface.OverlayScreens.SubScreens.Abstraction;
using Menus;

namespace Interface.OverlayScreens.SubScreens
{
    public class LevelSelectScreen : SubScreen
    {
        private LevelSelectLevelList _levelSelectList;

        protected override void Awake()
        {
            _levelSelectList = CreateGameObject.CreateChildGameObject<LevelSelectLevelList>(transform).GetComponent<LevelSelectLevelList>();
            _levelSelectList.enabled = false;
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateGridLayoutGroup();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.ParentScreen);
        }
    }
}
