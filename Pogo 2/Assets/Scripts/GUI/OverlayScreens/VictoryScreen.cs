using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public class VictoryScreen : OverlayScreen
    {
        protected override void Start()
        {
            _canvas = GameObject.Find("VictoryScreen");
            _canvas.SetActive(false);
        }
    }
}
