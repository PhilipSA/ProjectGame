using System;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Text;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
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
            _text.rectTransform.anchorMin = new Vector2(1, 1);
            _text.rectTransform.anchorMax = new Vector2(1, 1);
            _text.rectTransform.pivot = new Vector2(1, 1);
            _text.color = Color.white;
            base.Awake();
        }
    }
}
