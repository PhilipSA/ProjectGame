using System;
using UnityEngine;

namespace Assets.Scripts.CustomComponents
{
    public class StopWatch
    {
        public float TimeSinceStarted { get; private set; }   
        public bool Enabled { get; set; }

        public StopWatch(bool enabled)
        {
            Enabled = enabled;
        }
	
        // Update is called once per frame
        public void Tick()
        {
            if (Enabled) TimeSinceStarted += Time.deltaTime;
        }

        public string GetTimeInMmssffFormat()
        {
            var timeSpan = TimeSpan.FromSeconds(TimeSinceStarted);
            return new DateTime(timeSpan.Ticks).ToString("mm:ss:ff");
        }
    }
}
