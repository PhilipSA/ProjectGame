using GameObjects;
using GameObjects.Components.Controls.Sliders;
using Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Interface.InterfaceElements
{
    public class HealthBar : InterfaceElement {

        public float BarDisplay; //current progress
        public HealthBarSlider HealthBarSlider { get; private set; }

        protected override void Awake()
        {
            HealthBarSlider = CreateGameObject.CreateChildGameObject<HealthBarSlider>(transform).GetComponent<HealthBarSlider>();
            HealthBarSlider.Slider.maxValue = 100;
            HealthBarSlider.RectTransform.sizeDelta = new Vector2(300, 20);

            base.Awake();
        }

        void Start()
        {
            HealthBarSlider.RectTransform.anchorMin = new Vector2(0.5f, 0.95f);
            HealthBarSlider.RectTransform.anchorMax = new Vector2(0.5f, 0.95f);
        }

        void Update()
        {
            HealthBarSlider.Slider.value = BarDisplay;
        }
    }
}
