using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class PauseScreen : OverlayScreen
    {
        protected override void Start()
        {
            _canvas = GameObject.Find("PauseScreen");
            _canvas.SetActive(false);
        } 
    }
}
