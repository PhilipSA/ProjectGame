using System;
using GameObjects;
using GameObjects.Components.Controls.Text;
using Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Interface.InterfaceElements
{
    public class BestTimeDisplay : InterfaceElement
    {
        public float BestTime { get; private set; }
        public string BestTimeDisplayValue { get; private set; }
        private ControlText _text;

        public void SetTime(float time)
        {
            BestTime = time;
            var timeSpan = TimeSpan.FromSeconds(BestTime);
            BestTimeDisplayValue = new DateTime(timeSpan.Ticks).ToString("mm:ss:ff");
            _text.text = BestTimeDisplayValue;
        }

        protected override void Awake()
        {
            _text = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            base.Awake();
        }

        protected void Start()
        {
            _text.SetAnchorsAndPivot(_text.rectTransform.anchorMin = new Vector2(1, 1), new Vector2(1, 1), new Vector2(1, 1));
            _text.color = Color.white;
        }
    }
}
