using UnityEngine;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public abstract class OverlayScreen : MonoBehaviour
    {
        public bool IsVisible { get; private set; }
        public Canvas Canvas { get; private set; }

        protected virtual void Start()
        {
            Canvas = gameObject.GetComponent<Canvas>();
            Canvas.enabled = false;
        }

        public virtual void SetVisibility(bool visible)
        {
            IsVisible = visible;
            gameObject.GetComponent<Canvas>().enabled = visible;
        }
    }
}
