using GameObjects.Components.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Components.Controls.Sliders.Abstractions
{
    public abstract class BaseSlider : MonoBehaviour, IRectTransformAble
    {
        public RectTransform RectTransform { get; private set; }
        public Slider Slider;

        protected virtual void Awake()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Controls/Sliders/Slider");
            var clone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(this.transform);
            Slider = clone.GetComponent<Slider>();

            RectTransform = gameObject.AddComponent<RectTransform>();
            RectTransform.sizeDelta = new Vector2(200, 50);
        }

        protected virtual void Start()
        {
        
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
