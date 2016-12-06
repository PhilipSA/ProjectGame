using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Buttons
{
    public class ExitButton : Button
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }
}
