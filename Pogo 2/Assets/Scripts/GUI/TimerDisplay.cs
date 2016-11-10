using System;
using Assets.CustomComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class TimerDisplay : MonoBehaviour
    {
        public Timer timer;

        private Text _text;

        // Use this for initialization
        void Start()
        {
            _text = GetComponent<Text>();
            timer = new Timer();
        }

        // Update is called once per frame
        void Update()
        {
            timer.Tick();
            _text.text = timer.GetTimeInMmssffFormat();
        }

        void OnGUI()
        {

        }
    }
}
