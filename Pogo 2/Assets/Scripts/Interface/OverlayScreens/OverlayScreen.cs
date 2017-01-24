using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.OverlayScreens
{
    public abstract class OverlayScreen : MonoBehaviour
    {
        public bool IsVisible { get; private set; }
        public Canvas Canvas { get; private set; }
        public GraphicRaycaster GraphicRaycaster { get; private set; }
        public LayoutGroup LayoutGroup;

        protected virtual void Start()
        {
            Canvas = gameObject.AddComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Canvas.enabled = false;

            GraphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }

        public virtual void SetVisibility(bool visible)
        {
            IsVisible = visible;
            gameObject.GetComponent<Canvas>().enabled = visible;
        }

        protected void CreateVerticalLayoutGroup()
        {
            LayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            LayoutGroup.padding = new RectOffset(100, 100, 50, 50);
            LayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        }

        protected abstract void CreateLayoutGroup();
    }
}
