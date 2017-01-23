﻿using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Buttons;
using Assets.Scripts.Interface.Controls.Text;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public class VictoryScreen : OverlayScreen
    {
        public ControlText ClearingTimeText;
        public ControlText VictoryText;
        public NextLevelButton NextLevelButton;
        public RestartLevelButton RestartLevelButton;
        public MainMenuButton MainMenuButton;

        protected override void Start()
        {
            ClearingTimeText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            VictoryText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            NextLevelButton = CreateGameObject.CreateChildGameObject<NextLevelButton>(transform).GetComponent<NextLevelButton>();
            RestartLevelButton = CreateGameObject.CreateChildGameObject<RestartLevelButton>(transform).GetComponent<RestartLevelButton>();
            MainMenuButton = CreateGameObject.CreateChildGameObject<MainMenuButton>(transform).GetComponent<MainMenuButton>();    
            base.Start();      
        }

        public void SetClearingTimeText(string text)
        {
            ClearingTimeText.text = text;
        }
    }
}
