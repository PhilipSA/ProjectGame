using Assets.Scripts.CustomComponents;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class TimerDisplay : MonoBehaviour
    {
        public StopWatch StopWatch;
        private string _text;

        // Use this for initialization
        void Start()
        {
            StopWatch = new StopWatch(true);
        }

        // Update is called once per frame
        void Update()
        {
            StopWatch.Tick();
            _text = StopWatch.GetTimeInMmssffFormat();
        }

        public void StopTimer()
        {
            StopWatch.Enabled = false;
        }

        void OnGUI()
        {
            int width = Screen.width, height = Screen.height;
            Rect rect = new Rect(0, 0, width/9, height * 2 / 40);

            var texture2D = new Texture2D((int) rect.width, (int) rect.height);
            texture2D.SetPixel(0, 0, Color.black);
            texture2D.wrapMode = TextureWrapMode.Repeat;
            texture2D.Apply();

            GUIStyle textStyle = new GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = height*2/50,
                normal = {textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f), background = texture2D}
            };

            UnityEngine.GUI.Box(rect, _text, textStyle);
        }
    }
}
