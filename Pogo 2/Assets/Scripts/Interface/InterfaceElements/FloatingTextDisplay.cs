using Assets.Scripts.CustomComponents;
using Assets.Scripts.GameObjects;
using Assets.Scripts.GameObjects.Components.Controls.Text;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
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
            StopWatch = new StopWatch(false);
            base.Awake();
        }

        public void Update()
        {
            StopWatch.Tick();

            if (StopWatch.TimeSinceStarted > 3)
            {
                StopWatch.Enabled = false;
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
            if (!StopWatch.Enabled)
            {
                ControlText.SetAnchors(new Vector2(ControlText.rectTransform.anchorMin.x, 0.1f), new Vector2(ControlText.rectTransform.anchorMax.x, 0.1f));
                StopWatch = new StopWatch(true);
                ControlText.text = text;
                Canvas.enabled = true;
            }
        }
    }
}
