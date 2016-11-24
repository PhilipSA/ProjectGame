using System;
using System.Linq;
using Assets.CustomComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class TimerDisplay : MonoBehaviour
    {
        public StopWatch StopWatch;
        private Text _text;

        // Use this for initialization
        void Start()
        {
            _text = (Text)GameObject.Find("TimerText").GetComponent(typeof(Text));
            StopWatch = new StopWatch(true);
        }

        // Update is called once per frame
        void Update()
        {
            StopWatch.Tick();
            _text.text = StopWatch.GetTimeInMmssffFormat();
        }

        public void StopTimer()
        {
            StopWatch.Enabled = false;
        }

        void OnGUI()
        {

        }
    }
}
