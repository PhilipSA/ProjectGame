﻿using Assets.Scripts.GameObjects;
using Assets.Scripts.GameObjects.Components.Controls.Sliders;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class HealthBar : InterfaceElement {

        public float BarDisplay; //current progress
        public HealthBarSlider HealthBarSlider { get; private set; }

        protected override void Awake()
        {
            HealthBarSlider = CreateGameObject.CreateChildGameObject<HealthBarSlider>(transform).GetComponent<HealthBarSlider>();
            HealthBarSlider.maxValue = 100;
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
            HealthBarSlider.value = BarDisplay;
        }
    }
}
