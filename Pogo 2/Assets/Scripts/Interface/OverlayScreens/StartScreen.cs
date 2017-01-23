using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class StartScreen : OverlayScreen
    {
        public LevelSelectButton LevelSelectButton { get; private set; }
        public OptionsButton OptionsButton { get; private set; }
        public StartButton StartButton { get; private set; }
        public ExitButton ExitButton { get; private set; }

        protected override void Start()
        {
            LayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();

            StartButton = CreateGameObject.CreateChildGameObject<StartButton>(transform).GetComponent<StartButton>();
            OptionsButton = CreateGameObject.CreateChildGameObject<OptionsButton>(transform).GetComponent<OptionsButton>();
            LevelSelectButton = CreateGameObject.CreateChildGameObject<LevelSelectButton>(transform).GetComponent<LevelSelectButton>();
            ExitButton = CreateGameObject.CreateChildGameObject<ExitButton>(transform).GetComponent<ExitButton>();
        }
    }
}
