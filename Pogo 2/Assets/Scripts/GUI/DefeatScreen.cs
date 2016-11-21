using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class DefeatScreen : MonoBehaviour
    {
        private bool _isVisible;
        private GameObject _canvas;
        // Use this for initialization
        void Start()
        {
            _canvas = GameObject.Find("DefeatScreen");
            _canvas.SetActive(false);
        }

        // Update is called once per frame
        void OnGui()
        {

        }

        public void SetVisibility(bool visible)
        {
            _isVisible = visible;
            _canvas.SetActive(visible);
        }
    }
}
