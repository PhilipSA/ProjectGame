using System.Collections.Generic;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class ChargeBar : InterfaceElement {

        public float BarDisplay; //current progress
        private Image _bar;
        private List<Image> _barsList;

        protected override void Awake()
        {
            _barsList = new List<Image>();

            _bar = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();
            _bar.rectTransform.anchorMin = new Vector2(0, 0);
            _bar.rectTransform.anchorMax = new Vector2(0, 0);
            _bar.rectTransform.pivot = new Vector2(0, 0);

            _barsList.Add(_bar);

            base.Awake();
        }

        void Update()
        {
        }
    }
}
