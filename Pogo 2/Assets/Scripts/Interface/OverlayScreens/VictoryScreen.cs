using GameObjects;
using GameObjects.Components.Controls.Buttons;
using GameObjects.Components.Controls.Text;
using Interface.OverlayScreens.Abstraction;

namespace Interface.OverlayScreens
{
    public class VictoryScreen : OverlayScreen
    {
        public ControlText ClearingTimeText;
        public ControlText VictoryText;
        public NextLevelButton NextLevelButton;
        public RestartLevelButton RestartLevelButton;
        public MainMenuButton MainMenuButton;

        protected override void Awake()
        {
            ClearingTimeText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            VictoryText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            NextLevelButton = CreateGameObject.CreateChildGameObject<NextLevelButton>(transform).GetComponent<NextLevelButton>();
            RestartLevelButton = CreateGameObject.CreateChildGameObject<RestartLevelButton>(transform).GetComponent<RestartLevelButton>();
            MainMenuButton = CreateGameObject.CreateChildGameObject<MainMenuButton>(transform).GetComponent<MainMenuButton>();    
            base.Awake();      
        }

        protected override void CreateLayoutGroup()
        {
            CreateGridLayoutGroup();
        }

        public void SetClearingTimeText(string text)
        {
            ClearingTimeText.text = text;
        }
    }
}
