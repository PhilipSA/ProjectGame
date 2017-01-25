using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.Controls.Text;
using Assets.Scripts.Interface.OverlayScreens.Abstraction;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class StartScreen : OverlayScreen
    {
        public ControlText Title { get; private set; }
        public LevelSelectButton LevelSelectButton { get; private set; }
        public OptionsButton OptionsButton { get; private set; }
        public StartButton StartButton { get; private set; }
        public ExitButton ExitButton { get; private set; }

        protected override void Awake()
        {
            Title = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            StartButton = CreateGameObject.CreateChildGameObject<StartButton>(transform).GetComponent<StartButton>();
            OptionsButton = CreateGameObject.CreateChildGameObject<OptionsButton>(transform).GetComponent<OptionsButton>();
            LevelSelectButton = CreateGameObject.CreateChildGameObject<LevelSelectButton>(transform).GetComponent<LevelSelectButton>();
            ExitButton = CreateGameObject.CreateChildGameObject<ExitButton>(transform).GetComponent<ExitButton>();
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
        }
    }
}
