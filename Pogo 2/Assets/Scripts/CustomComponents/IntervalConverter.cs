using InteractingObjects.Player;

namespace Assets.Scripts.CustomComponents
{
    public static class IntervalConverter
    {
        public static float ConvertValueInIntervalToOtherIntervalValue(float oldMin, float oldMax, float newMin, float newMax, float value)
        {
            return (value - oldMin) * (newMax - newMin) /
              (oldMax - oldMin) + newMin;
        }
    }
}
