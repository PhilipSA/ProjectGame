using Engine;
using Engine.Audio;
using InteractingObjects.Abstraction;
using UnityEngine;

namespace InteractingObjects.Contraptions
{
    public class Padding : AnimatedSprite
    {
        public AudioSource AudioSource;

        void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            base.Awake("/Textures/Contraptions/Padding");
        }

        void Start()
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/ContraptionsAudio/Padding");
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            AudioHandler.PlayAudio(AudioSource);
            GameEngineHelper.GetCurrentGameEngine().Player.PlayerCollider.OnPaddingCollision(col);
        }
    }
}
