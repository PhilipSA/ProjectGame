using Assets.Scripts.CustomComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class TimerDisplay : MonoBehaviour
    {
        public StopWatch StopWatch;
        private Text _text;

        // Use this for initialization
        void Start()
        {
            _text = GetComponentInChildren<Text>();
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

        void OnGui()
        {

        }
    }
}
