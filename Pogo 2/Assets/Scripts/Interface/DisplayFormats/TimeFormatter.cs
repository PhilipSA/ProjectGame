using System;

namespace Interface.DisplayFormats
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
