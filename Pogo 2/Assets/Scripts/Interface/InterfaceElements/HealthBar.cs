using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Sliders;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class HealthBar : InterfaceElement {

        public float BarDisplay; //current progress
        private HealthBarSlider _healthBarSlider;
        private Image graphic;

        protected override void Awake()
        {
            graphic = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();
            graphic.rectTransform.anchorMin = new Vector2(0.5f, 1);
            graphic.rectTransform.anchorMax = new Vector2(0.5f, 1);
            graphic.rectTransform.pivot = new Vector2(0.5f, 0.5f);

            _healthBarSlider = CreateGameObject.CreateChildGameObject<HealthBarSlider>(graphic.transform).GetComponent<HealthBarSlider>();
            _healthBarSlider.maxValue = 100;

            base.Awake();
        }

        void Update()
        {
            _healthBarSlider.value = BarDisplay;
        }
    }
}
