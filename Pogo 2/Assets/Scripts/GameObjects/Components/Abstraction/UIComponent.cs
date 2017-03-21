using UnityEngine;
using UnityEngine.EventSystems;

namespace GameObjects.Components.Abstraction
{
    public class UIComponent : UIBehaviour, IRectTransformAble
    {
        protected RectTransform RectTransform { get; private set; }

        protected override void Awake()
        {
            RectTransform = gameObject.AddComponent<RectTransform>();
        }

        public void SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
        }

        public void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
            RectTransform.pivot = pivot;
        }
    }
}
