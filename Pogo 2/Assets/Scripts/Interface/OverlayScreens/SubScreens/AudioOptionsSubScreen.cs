﻿using GameObjects;
using GameObjects.Components.Controls.OptionBox.Audio;
using Interface.OverlayScreens.SubScreens.Abstraction;
using Menus;

namespace Interface.OverlayScreens.SubScreens
{
    public class AudioOptionsSubScreen : OptionsSubScreen
    {
        private MusicVolumeOptionBox _musicVolumeLocalizeableOptionBox;
        private SoundEffectVolumeOptionBox _soundEffectVolumeLocalizeableOptionBox;

        protected override void Awake()
        {
            _musicVolumeLocalizeableOptionBox = CreateGameObject.CreateChildGameObject<MusicVolumeOptionBox>(transform).GetComponent<MusicVolumeOptionBox>();
            _soundEffectVolumeLocalizeableOptionBox = CreateGameObject.CreateChildGameObject<SoundEffectVolumeOptionBox>(transform).GetComponent<SoundEffectVolumeOptionBox>();
            base.Awake();
        }

        protected override void CreateLayoutGroup()
        {
            CreateVerticalLayoutGroup();
        }

        protected override void OnApplyButtonClick()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnBackButtonClick()
        {
            MenuHelper.GetCurrentMenu().ChangeCurrentActiveScreen(MenuHelper.GetCurrentMenu().OptionsScreen);
        }
    }
}
