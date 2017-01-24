using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.Controls.Text;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class DefeatScreen : OverlayScreen
    {
        public ControlText Text;
        public MainMenuButton MainMenuButton;
        public RestartLevelButton RestartLevelButton;

        protected override void Start()
        {
            Text = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            MainMenuButton = CreateGameObject.CreateChildGameObject<MainMenuButton>(transform).GetComponent<MainMenuButton>();
            RestartLevelButton = CreateGameObject.CreateChildGameObject<RestartLevelButton>(transform).GetComponent<RestartLevelButton>();
            base.Start();
        }

        protected override void CreateLayoutGroup()
        {
            throw new System.NotImplementedException();
        }
    }
}
