using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GUI.DisplayFormats
{
    public static class TimeFormatter
    {
        public static string GetTimeInMmssffFormat(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return new DateTime(timeSpan.Ticks).ToString("mm:ss:ff");
        }
    }
}
