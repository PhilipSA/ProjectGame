using Engine;
using Engine.Audio;
using InteractingObjects.Abstraction;
using UnityEngine;

namespace InteractingObjects.Contraptions
{
    public class Trampoline : AnimatedSprite
    {
        public AudioSource AudioSource;
        public float Force = 30000;

        void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            base.Awake("/Textures/Contraptions/Trampoline");
        }

        void Start()
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/ContraptionsAudio/Trampoline");
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            AudioHandler.PlayAudio(AudioSource);
            GameEngineHelper.GetCurrentGameEngine().Player.PlayerCollider.OnTrampolineCollision(col, Force);
        }
    }
}
