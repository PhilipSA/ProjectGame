using System;
using System.Linq;
using Enums.Screen;
using GameObjects.Components.Controls.Dropdowns;
using GameObjects.Components.Controls.OptionBox.Abstraction;
using SmartLocalization;
using UnityEngine.UI;

namespace GameObjects.Components.Controls.OptionBox.Graphics
{
    public class ScreenTypeOptionBox : LocalizeableOptionBox
    {
        public ScreenTypeDropdown ScreenTypeDropdown { get; private set; }

        protected override void Start()
        {
            ScreenTypeDropdown = CreateGameObject.CreateChildGameObject<ScreenTypeDropdown>(transform).GetComponent<ScreenTypeDropdown>(); ;
            DisplayText = LanguageManager.Instance.GetTextValue("ScreenType");
            AddAllScreenTypesToDropdown();
            base.Start();
        }

        public void AddAllScreenTypesToDropdown()
        {
            var optionItems = from screenType in Enum.GetValues(typeof(ScreenTypeEnum)).Cast<ScreenTypeEnum>()
                select new Dropdown.OptionData {text = screenType.ToString()};
            ScreenTypeDropdown.Dropdown.options.AddRange(optionItems);
        }

        public bool IsFullscreen()
        {
            return ScreenTypeDropdown.Dropdown.value == (int)ScreenTypeEnum.FullScreen;
        }
    }
}
