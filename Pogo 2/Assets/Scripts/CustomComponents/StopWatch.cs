using System;
using Assets.Scripts.GUI.DisplayFormats;
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
            return TimeFormatter.GetTimeInMmssffFormat(TimeSinceStarted);
        }
    }
}
