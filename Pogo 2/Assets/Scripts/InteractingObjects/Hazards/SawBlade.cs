using Assets.Scripts.Engine;
using Assets.Scripts.GameObjects.Components.Animation;
using Assets.Scripts.Engine.Audio;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Hazards
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
            GameEngineHelper.GetCurrentGameEngine().Player.OnHazardCollision();
        }

        public void Update()
        {
            transform.Rotate(0, 0, 2);
        }
    }
}
