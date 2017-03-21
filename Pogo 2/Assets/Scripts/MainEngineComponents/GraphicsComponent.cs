using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MainEngineComponents
{
    public class GraphicsComponent
    {
        public IEnumerable<Resolution> GetAllSupportedResolutions()
        {
            return Screen.resolutions.Distinct();
        }

        public Dropdown.OptionData ConvertResolutionToOptionData(Resolution resolution)
        {
            return new Dropdown.OptionData
            {
                text = resolution.ToString()
            };
        }

        public void SetResolution(Resolution resolution, bool fullScreen)
        {
            Screen.SetResolution(resolution.width, resolution.height, fullScreen);
        }
    }
}
