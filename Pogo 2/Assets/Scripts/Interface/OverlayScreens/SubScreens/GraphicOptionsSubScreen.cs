﻿using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.OptionBox.Graphics;
using Assets.Scripts.Interface.OverlayScreens.SubScreens.Abstraction;
using Assets.Scripts.MainEngineComponents;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Interface.OverlayScreens.SubScreens
{
    public class GraphicOptionsSubScreen : OptionsSubScreen
    {
        private ResolutionOptionBox _resolutionOptionBox;
        private ScreenTypeOptionBox _screenTypeOptionBox;

        protected override void Awake()
        {
            _resolutionOptionBox = CreateGameObject.CreateChildGameObject<ResolutionOptionBox>(transform).GetComponent<ResolutionOptionBox>();
            _screenTypeOptionBox = CreateGameObject.CreateChildGameObject<ScreenTypeOptionBox>(transform).GetComponent<ScreenTypeOptionBox>();
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
        }

        protected override void OnBackButtonClick()
        {
            Menu.ChangeCurrentActiveScreen(Menu.OptionsScreen);
        }

        protected override void OnApplyButtonClick()
        {
            MainEngine.GetMainEngine.GraphicsComponent.SetResolution(_resolutionOptionBox.GetSelectedResolution(), _screenTypeOptionBox.IsFullscreen());
        }
    }
}
