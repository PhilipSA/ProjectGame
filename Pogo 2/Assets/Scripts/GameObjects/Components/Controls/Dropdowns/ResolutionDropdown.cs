using System.Collections.Generic;
using GameObjects.Components.Controls.Dropdowns.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Components.Controls.Dropdowns
{
    public class ResolutionDropdown : BaseDropDown
    {
        public Dictionary<int, Resolution> MappedValues { get; private set; }

        protected override void Awake()
        {
            MappedValues = new Dictionary<int, Resolution>();
            base.Awake();
        }

        public void AddOption(Dropdown.OptionData optionData, Resolution resolution)
        {
            MappedValues.Add(Dropdown.options.Count + 1, resolution);
            Dropdown.options.Add(optionData);
        }
    }
}
