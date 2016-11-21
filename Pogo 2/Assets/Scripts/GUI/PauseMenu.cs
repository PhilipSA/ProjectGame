using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class PauseMenu : MonoBehaviour
    {
        private bool _isVisible;
        private GameObject _canvas;
        // Use this for initialization
        void Start()
        {
            _canvas = GameObject.Find("PauseMenu");
            _canvas.SetActive(false);
        }

        public void SetVisibility(bool visible)
        {
            _isVisible = visible;
            _canvas.SetActive(visible);
        }
    }
}
