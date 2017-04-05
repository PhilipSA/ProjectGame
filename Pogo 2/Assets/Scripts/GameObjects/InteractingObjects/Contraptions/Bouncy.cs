using Engine;
using Engine.Audio;
using InteractingObjects.Abstraction;
using UnityEngine;

namespace InteractingObjects.Contraptions
{
    public class Bouncy : AnimatedSprite
    {
        public AudioSource AudioSource;
        public float Force = 350;

        void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            base.Awake("/Textures/Contraptions/Bouncy");
        }

        void Start()
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/ContraptionsAudio/Bouncy");
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            AudioHandler.PlayAudio(AudioSource);
            GameEngineHelper.GetCurrentGameEngine().Player.PlayerCollider.OnBouncyCollision(col, Force);
        }
    }
}
