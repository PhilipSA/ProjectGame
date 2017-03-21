using Interface.DisplayFormats;
using UnityEngine;

namespace CustomComponents
{
    public class StopWatch : MonoBehaviour
    {
        public float TimeSinceStarted { get; private set; }

        void Awake()
        {
            enabled = false;
        }

        public void StartTimer()
        {
            TimeSinceStarted = 0;
            enabled = true;
        }

        public void StopTimer()
        {
            enabled = false;
        }
	
        // Update is called once per frame
        public void Update()
        {
            TimeSinceStarted += Time.deltaTime;
        }

        public string GetTimeInMmssffFormat()
        {
            return TimeFormatter.GetTimeInMmssffFormat(TimeSinceStarted);
        }
    }
}
