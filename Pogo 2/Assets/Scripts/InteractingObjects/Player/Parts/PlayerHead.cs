using Assets.Scripts.Engine.Audio;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects.Components.Animation;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player.Parts
{
    public class PlayerHead : MonoBehaviour
    {
        private Player _parent;
        private Rigidbody2D _headRigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private AnimationSprite _animationHandler;
        private AudioSource _audioSource;

        void Awake()
        {
            _headRigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _parent = transform.parent.GetComponent<Player>();

            _animationHandler = gameObject.AddComponent<AnimationSprite>();
            _animationHandler.Location = "Textures/Player/PlayerHead/";
        }

        void Start()
        {
            _headRigidbody2D.freezeRotation = true;
        }

        void Update()
        {
            if (_headRigidbody2D.velocity.magnitude > 200)
            {
                Debug.Log(_headRigidbody2D.velocity.magnitude);
                _animationHandler.SetBlinkSprites("headHighRes", "headHighResExcited");
                _animationHandler.AnimationType = AnimationTypeEnum.Blink;
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (_parent.enabled)
            {
                _animationHandler.SetBlinkSprites("headHighRes", "headHighResDamage");
                _parent.OnHeadCollision();
                _animationHandler.AnimationType = AnimationTypeEnum.Blink;
                AudioHandler.PlayAudio(_audioSource);
            }
        }
    }
}
