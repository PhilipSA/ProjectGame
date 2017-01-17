using UnityEngine;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public abstract class OverlayScreen : MonoBehaviour
    {
        public bool IsVisible { get; private set; }

        protected virtual void Start()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetVisibility(bool visible)
        {
            IsVisible = visible;
            gameObject.SetActive(visible);
        }
    }
}
