using Assets.Scripts.Engine;
using Assets.Scripts.Engine.Audio;
using Assets.Scripts.InteractingObjects.Abstraction;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions
{
    public class Trampoline : AnimatedSprite
    {
        public AudioSource AudioSource;
        public float Force = 350;

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
