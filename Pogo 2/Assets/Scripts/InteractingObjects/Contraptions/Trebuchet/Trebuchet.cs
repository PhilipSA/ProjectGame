using CustomComponents;
using UnityEngine;

namespace InteractingObjects.Contraptions.Trebuchet
{
    public class Trebuchet : MonoBehaviour
    {
        public Counterweigh Counterweigh;
        public Sling Sling;
        public bool Firing;
        public StopWatch ResetTimer;

        public void Awake()
        {
            Counterweigh = GetComponentInChildren<Counterweigh>();
            Sling = GetComponentInChildren<Sling>();
            ResetTimer = gameObject.AddComponent<StopWatch>();
        }

        public void Update()
        {
            if (Firing && ResetTimer.TimeSinceStarted > 2)
            {
                Reset();
                Firing = false;
            }
        }

        public void FIRREEEE()
        {
            Counterweigh.AddMass(8000);
            Firing = true;
            ResetTimer.StartTimer();
        }

        public void Reset()
        {
            ResetTimer.StopTimer();
            Counterweigh.ResetMass();
        }
    }
}
