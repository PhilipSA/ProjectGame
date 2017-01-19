using System;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class BestTimeDisplay : MonoBehaviour
    {
        public float BestTime { get; private set; }
        public string BestTimeDisplayValue { get; private set; }

        public void SetTime(float time)
        {
            BestTime = time;
            var timeSpan = TimeSpan.FromSeconds(BestTime);
            BestTimeDisplayValue = new DateTime(timeSpan.Ticks).ToString("mm:ss:ff");
        }

        void OnGUI()
        {
            int width = Screen.width, height = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, width, height * 2 / 50);
            style.alignment = TextAnchor.UpperRight;
            style.fontSize = height * 2 / 50;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            UnityEngine.GUI.Label(rect, BestTimeDisplayValue, style);
        }
    }
}
