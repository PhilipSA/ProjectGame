using GameObjects.Components.Controls.Sliders.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Components.Controls.Sliders
{
    public class HealthBarSlider : BaseNonInteractableSlider
    {
        protected override void Awake()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Controls/Sliders/HealthBarSlider");
            var clone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(transform);
            Slider = clone.GetComponent<Slider>();

            RectTransform = gameObject.AddComponent<RectTransform>();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}
