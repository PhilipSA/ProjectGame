using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class PauseScreen : OverlayScreen
    {
        protected override void Start()
        {
            _canvas = GameObject.Find("PauseMenu");
            _canvas.SetActive(false);
        }
    }
}
