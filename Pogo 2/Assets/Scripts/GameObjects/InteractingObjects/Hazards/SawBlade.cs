using Engine;
using Engine.Audio;
using GameObjects.Components.Animation;
using UnityEngine;

namespace GameObjects.InteractingObjects.Hazards
{
    public class SawBlade : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteAnimation _animationHandler;
        public AudioSource AudioSource;

        public void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/HazardsAudio/SawBladeIdle");
            AudioSource.loop = true;            
        }

        public void Start()
        {
            AudioHandler.PlayAudio(AudioSource);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/HazardsAudio/SawBladeDamage");
            AudioHandler.PlayAudio(AudioSource);
            GameEngineHelper.GetCurrentGameEngine().Player.PlayerCollider.OnHazardCollision(col);
        }

        void OnCollisionExit2D(Collision2D col)
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/HazardsAudio/SawBladeIdle");
        }

        public void Update()
        {
            transform.Rotate(0, 0, 2);
        }
    }
}
