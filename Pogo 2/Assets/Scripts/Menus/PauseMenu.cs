using GameObjects;
using Interface.OverlayScreens;
using UnityEngine;

namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public PauseScreen PauseScreen;
        public Menu Menu;

        void Start()
        {
            PauseScreen = CreateGameObject.CreateChildGameObject<PauseScreen>(transform).GetComponent<PauseScreen>();
            Menu = CreateGameObject.CreateChildGameObject<Menu>(transform).GetComponent<Menu>();
            Menu.SetParentScreen(PauseScreen);
        }

        public void ToggleDisplay(bool toggle)
        {
            MenuHelper.GetCurrentMenu().ChangeCurrentActiveScreen(toggle ? PauseScreen : null);
        }
    }
}
