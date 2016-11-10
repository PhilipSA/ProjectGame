using System;
using UnityEngine;

namespace Assets.CustomComponents
{
    public class Timer
    {
        public float TimeSinceStarted { get; private set; }        
	
        // Update is called once per frame
        public void Tick()
        {
            TimeSinceStarted += Time.deltaTime;
        }

        public string GetTimeInMmssffFormat()
        {
            var timeSpan = TimeSpan.FromSeconds(TimeSinceStarted);
            return new DateTime(timeSpan.Ticks).ToString("mm:ss:ff");
        }
    }
}
