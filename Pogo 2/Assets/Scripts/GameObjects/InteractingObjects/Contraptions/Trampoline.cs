using Engine;
using Engine.Audio;
using GameObjects.InteractingObjects.Abstraction;
using UnityEngine;

namespace GameObjects.InteractingObjects.Contraptions
{
    public class Trampoline : AnimatedSprite
    {
        public AudioSource AudioSource;
        public float Force = 35000;

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
