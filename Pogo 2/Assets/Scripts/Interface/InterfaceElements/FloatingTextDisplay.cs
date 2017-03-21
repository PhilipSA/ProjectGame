using CustomComponents;
using GameObjects;
using GameObjects.Components.Controls.Text;
using Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Interface.InterfaceElements
{
    public class FloatingTextDisplay : InterfaceElement
    {
        public ControlText ControlText { get; private set; }
        public StopWatch StopWatch { get; private set; }

        protected override void Awake()
        {
            ControlText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            ControlText.color = Color.white;
            ControlText.rectTransform.sizeDelta = new Vector2(300, 50);
            StopWatch = gameObject.AddComponent<StopWatch>();
            base.Awake();
        }

        public void Update()
        {
            if (StopWatch.TimeSinceStarted > 3)
            {
                StopWatch.StopTimer();
                Canvas.enabled = false;
            }
            else
            {
                ControlText.SetAnchors(new Vector2(ControlText.rectTransform.anchorMin.x, ControlText.rectTransform.anchorMin.y + StopWatch.TimeSinceStarted / 1000), 
                    new Vector2(ControlText.rectTransform.anchorMax.x, ControlText.rectTransform.anchorMax.y + StopWatch.TimeSinceStarted / 1000));
            }
        }

        public void SetAndEnable(string text)
        {
            if (!StopWatch.enabled)
            {
                ControlText.SetAnchors(new Vector2(ControlText.rectTransform.anchorMin.x, 0.1f), new Vector2(ControlText.rectTransform.anchorMax.x, 0.1f));
                StopWatch.StartTimer();
                ControlText.text = text;
                Canvas.enabled = true;
            }
        }
    }
}
