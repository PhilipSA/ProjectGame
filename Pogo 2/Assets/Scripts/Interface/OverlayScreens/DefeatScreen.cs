using GameObjects;
using GameObjects.Components.Controls.Buttons;
using GameObjects.Components.Controls.Text;
using Interface.OverlayScreens.Abstraction;

namespace Interface.OverlayScreens
{
    public class DefeatScreen : OverlayScreen
    {
        public ControlText Text;
        public MainMenuButton MainMenuButton;
        public RestartLevelButton RestartLevelButton;

        protected override void Awake()
        {
            Text = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            MainMenuButton = CreateGameObject.CreateChildGameObject<MainMenuButton>(transform).GetComponent<MainMenuButton>();
            RestartLevelButton = CreateGameObject.CreateChildGameObject<RestartLevelButton>(transform).GetComponent<RestartLevelButton>();
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateGridLayoutGroup();
        }
    }
}
