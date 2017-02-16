using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Dropdowns.Abstractions
{
    public abstract class BaseDropDown : MonoBehaviour
    {
        public RectTransform RectTransform;
        public Dropdown Dropdown;

        protected virtual void Awake()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Controls/Dropdowns/Dropdown");
            var clone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            clone.transform.parent = this.transform;
            Dropdown = clone.GetComponent<Dropdown>();
        }

        protected virtual void Start()
        {
        }

        public void SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            throw new System.NotImplementedException();
        }

        public void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            throw new System.NotImplementedException();
        }
    }
}
