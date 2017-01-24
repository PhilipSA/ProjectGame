using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.Menus;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class LevelSelectScreen : SubScreen
    {
        private LevelSelectLevelList _levelSelectList;

        protected override void Start()
        {
            _levelSelectList = CreateGameObject.CreateChildGameObject<LevelSelectLevelList>(transform).GetComponent<LevelSelectLevelList>();
            _levelSelectList.enabled = false;
            base.Start();
        }

        protected override void CreateLayoutGroup()
        {
            LayoutGroup = gameObject.AddComponent<GridLayoutGroup>();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.ParentScreen);
        }
    }
}
