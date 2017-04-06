using CustomComponents;
using Engine.Audio;
using UnityEngine;

namespace GameObjects.InteractingObjects.Contraptions.Trebuchet
{
    public class Trebuchet : MonoBehaviour
    {
        public Counterweigh Counterweigh;
        public Sling Sling;
        public Beam Beam;
        public bool Firing;
        public StopWatch ResetTimer;
        public AudioSource AudioSource;
        public float Force = 80000;

        public void Awake()
        {
            Counterweigh = GetComponentInChildren<Counterweigh>();
            Sling = GetComponentInChildren<Sling>();
            Beam = GetComponentInChildren<Beam>();
            ResetTimer = gameObject.AddComponent<StopWatch>();
            AudioSource = gameObject.GetComponent<AudioSource>();
        }

        void Start()
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/ContraptionsAudio/Trebuchet_NONLICENSED");
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
            if (!Firing)
            {
                AudioHandler.PlayAudio(AudioSource);
                Counterweigh.AddForce(Force);
                Firing = true;
                ResetTimer.StartTimer();
            }
        }

        public void Reset()
        {
            Beam.transform.Rotate(0f, 0f, 0f);
            ResetTimer.StopTimer();
            Counterweigh.ResetMass();
        }
    }
}
