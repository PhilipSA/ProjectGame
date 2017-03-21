using Assets.Scripts.InteractingObjects.Contraptions.Trebuchet;
using CustomComponents;
using UnityEngine;

namespace InteractingObjects.Contraptions.Trebuchet
{
    public class Trebuchet : MonoBehaviour
    {
        public Counterweigh Counterweigh;
        public Sling Sling;
        public Beam Beam;
        public bool Firing;
        public StopWatch ResetTimer;

        public void Awake()
        {
            Counterweigh = GetComponentInChildren<Counterweigh>();
            Sling = GetComponentInChildren<Sling>();
            Beam = GetComponentInChildren<Beam>();
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
            Counterweigh.AddMass(10000);
            Firing = true;
            ResetTimer.StartTimer();
        }

        public void Reset()
        {
            Beam.transform.Rotate(0f, 0f, 0f);
            ResetTimer.StopTimer();
            Counterweigh.ResetMass();
        }
    }
}
