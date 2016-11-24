using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class OptionsScreen : OverlayScreen
    {
        protected override void Start()
        {
            _canvas = GameObject.Find("OptionsScreen");
            _canvas.SetActive(false);
        }
    }
}
