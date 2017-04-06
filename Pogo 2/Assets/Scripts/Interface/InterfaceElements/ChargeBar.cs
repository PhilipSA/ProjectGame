using System.Collections.Generic;
using System.Linq;
using CustomComponents;
using Engine;
using GameObjects;
using GameObjects.Components.Image;
using GameObjects.InteractingObjects.Player;
using Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Interface.InterfaceElements
{
    public class ChargeBar : InterfaceElement {

        public float BarDisplay { get; private set; } //current progress
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
            BarDisplay =
                IntervalConverter.ConvertValueInIntervalToOtherIntervalValue(PlayerBounceLogic.MinimumBouncePower,
                    PlayerBounceLogic.MaximumBouncePower, 1, 10,
                    GameEngineHelper.GetCurrentGameEngine().Player.PlayerBounceLogic.GetBouncePower());
            foreach (var bar in _barsList)
            {
                bar.enabled = _barsList.IndexOf(bar) <= BarDisplay;
            }
        }
    }
}
