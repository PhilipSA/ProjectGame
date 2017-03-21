using GameObjects.Components.Controls.Dropdowns;
using GameObjects.Components.Controls.OptionBox.Abstraction;
using MainEngineComponents;
using SmartLocalization;
using UnityEngine;

namespace GameObjects.Components.Controls.OptionBox.Graphics
{
    public class ResolutionOptionBox : LocalizeableOptionBox
    {
        public ResolutionDropdown ResolutionDropdown { get; private set; }

        protected override void Start()
        {
            ResolutionDropdown = CreateGameObject.CreateChildGameObject<ResolutionDropdown>(transform).GetComponent<ResolutionDropdown>();
            DisplayText = LanguageManager.Instance.GetTextValue("Resolution");
            AddAllSupportedResolutionToDropdown();
            base.Start();
        }

        public void AddAllSupportedResolutionToDropdown()
        {
            foreach (var resolution in MainEngine.GetMainEngine.GraphicsComponent.GetAllSupportedResolutions())
                ResolutionDropdown.AddOption(MainEngine.GetMainEngine.GraphicsComponent.ConvertResolutionToOptionData(resolution), resolution);
        }

        public Resolution GetSelectedResolution()
        {
            Resolution resoltuion;
            ResolutionDropdown.MappedValues.TryGetValue(ResolutionDropdown.Dropdown.value, out resoltuion);
            return resoltuion;
        }
    }
}
