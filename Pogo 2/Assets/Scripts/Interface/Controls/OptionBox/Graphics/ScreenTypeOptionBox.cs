using System;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Interface.Controls.Dropdowns;
using Assets.Scripts.Interface.Controls.OptionBox.Abstraction;
using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.OptionBox.Graphics
{
    public class ScreenTypeOptionBox : LocalizeableOptionBox
    {
        public ScreenTypeDropdown ScreenTypeDropdown { get; private set; }

        protected override void Start()
        {
            ScreenTypeDropdown = GetComponentInChildren<ScreenTypeDropdown>();
            DisplayText = LanguageManager.Instance.GetTextValue("ScreenType");
            AddAllScreenTypesToDropdown();
            base.Start();
        }

        public void AddAllScreenTypesToDropdown()
        {
            var optionItems = from screenType in Enum.GetValues(typeof(ScreenTypeEnum)).Cast<ScreenTypeEnum>()
                select new Dropdown.OptionData {text = screenType.ToString()};
            Debug.Log(optionItems.Count());
            ScreenTypeDropdown.options.AddRange(optionItems);
        }

        public bool IsFullscreen()
        {
            return ScreenTypeDropdown.value == (int)ScreenTypeEnum.FullScreen;
        }
    }
}
