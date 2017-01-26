using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Sliders;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class HealthBar : InterfaceElement {

        public float BarDisplay; //current progress
        private HealthBarSlider _healthBarSlider;

        protected override void Awake()
        {
            _healthBarSlider = CreateGameObject.CreateChildGameObject<HealthBarSlider>(transform).GetComponent<HealthBarSlider>();
            _healthBarSlider.maxValue = 100;
            _healthBarSlider.RectTransform.sizeDelta = new Vector2(300, 20);

            base.Awake();
        }

        void Start()
        {
            _healthBarSlider.RectTransform.anchorMin = new Vector2(0.5f, 0.95f);
            _healthBarSlider.RectTransform.anchorMax = new Vector2(0.5f, 0.95f);
        }

        void Update()
        {
            _healthBarSlider.value = BarDisplay;
        }
    }
}
