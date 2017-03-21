using UnityEngine;
using UnityEngine.UI;

namespace Interface.InterfaceElements.Abstraction
{
    public abstract class InterfaceElement : MonoBehaviour
    {
        protected Canvas Canvas;
        protected CanvasScaler CanvasScaler;

        protected virtual void Awake()
        {
            Canvas = gameObject.AddComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler = gameObject.AddComponent<CanvasScaler>();
            CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
    }
}
