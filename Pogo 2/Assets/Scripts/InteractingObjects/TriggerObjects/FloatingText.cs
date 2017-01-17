using Assets.Scripts.CustomComponents;
using SmartLocalization;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.TriggerObjects
{
    public class FloatingText : TriggerObject
    {
        public string DisplayText;
        public StopWatch StopWatch { get; private set; }

        public void Start()
        {
            StopWatch = new StopWatch(false);
            enabled = false;
        }

        public void OnGui()
        {
            StopWatch.Tick();

            if (StopWatch.TimeSinceStarted < 10)
            {
                var style = new GUIStyle
                {
                    fontSize = Screen.height * 2 / 50,
                    normal = {textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f)}
                };
                var position = Camera.main.WorldToScreenPoint(TriggerCollider2D.transform.position);
                Rect rect = new Rect(new Vector2(position.x, position.y - StopWatch.TimeSinceStarted/100), TriggerCollider2D.size/5);
                UnityEngine.GUI.Label(rect, LanguageManager.Instance.GetTextValue(DisplayText), style);
            }
            else
            {
                StopWatch.Enabled = false;
                enabled = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            StopWatch.Enabled = true;
            enabled = true;
        }
    }
}
