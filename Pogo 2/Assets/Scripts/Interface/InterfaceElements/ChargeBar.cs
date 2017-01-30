using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameObjects;
using Assets.Scripts.GameObjects.Components.Image;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class ChargeBar : InterfaceElement {

        public int BarDisplay; //current progress
        public CustomImage Bar { get; private set; }
        private List<CustomImage> _barsList;

        protected override void Awake()
        {
            _barsList = new List<CustomImage>();

            for (float i = 0.1f; i < 0.5f; i += 0.05f)
            {
                Bar = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();
                Bar.color = Color.yellow;
                Bar.rectTransform.sizeDelta = new Vector2(50, 10);
                Bar.SetAnchorsAndPivot(new Vector2(0, i), new Vector2(0, i), new Vector2(0, 0));
                Bar.enabled = false;
                _barsList.Add(Bar);
            }

            base.Awake();
        }

        void Start()
        {
            _barsList.First().enabled = true;
        }

        void Update()
        {
            foreach (var bar in _barsList)
            {
                bar.enabled = _barsList.IndexOf(bar) <= BarDisplay;
            }
        }
    }
}
