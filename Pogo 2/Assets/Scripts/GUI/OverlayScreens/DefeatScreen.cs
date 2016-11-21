using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class DefeatScreen : OverlayScreen
    {
        protected override void Start()
        {
            _canvas = GameObject.Find("DefeatScreen");
            _canvas.SetActive(false);
        }
    }
}
