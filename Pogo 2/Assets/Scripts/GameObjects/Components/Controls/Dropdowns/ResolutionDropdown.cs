using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns
{
    public class ResolutionDropdown : Dropdown
    {
        public Dictionary<int, Resolution> MappedValues { get; private set; }

        protected override void Awake()
        {
            MappedValues = new Dictionary<int, Resolution>();
            base.Awake();
        }

        public void AddOption(OptionData optionData, Resolution resolution)
        {
            MappedValues.Add(options.Count + 1, resolution);
            options.Add(optionData);
        }
    }
}
