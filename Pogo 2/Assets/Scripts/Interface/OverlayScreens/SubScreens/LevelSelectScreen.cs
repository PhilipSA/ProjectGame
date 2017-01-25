using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
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
