using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Assets.Scripts.Engine;
using Assets.Scripts.Engine.Audio;
using Assets.Scripts.Enums;
using Assets.Scripts.Enums.Player.PlayerHead;
using Assets.Scripts.InteractingObjects.Abstraction;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player.Parts
{
    public class PlayerHead : AnimatedSprite
    {
        private Player _parent;
        private Rigidbody2D _headRigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;

        protected void Awake()
        {
            _headRigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _parent = transform.parent.GetComponent<Player>();

            base.Awake("Textures/Player/PlayerHead/");
        }

        void Start()
        {
            _headRigidbody2D.freezeRotation = true;
        }

        void Update()
        {
            if (_headRigidbody2D.velocity.magnitude > 200)
            {
                SpriteAnimation.AnimateBlink(EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighRes), EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighResExcited), 
                    PlayerHeadAnimationsEnum.HeadHighResExcited);
            }
            foreach (var collider in Physics2D.OverlapBoxAll(GameEngineHelper.GetCurrentGameEngine().MainCamera.transform.position, new Vector2(100, 100), 0))
            {
                if (Vector2.Distance(collider.bounds.ClosestPoint(transform.position), transform.position) < 25 && collider.name.Contains("Quad"))
                {
                    SpriteAnimation.AnimateBlink(EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighRes), EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighResPanic),
                        PlayerHeadAnimationsEnum.HeadHighResPanic);
                }
            }
            
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (_parent.enabled)
            {
                SpriteAnimation.AnimateBlink(EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighRes), EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighResDamage),
                        PlayerHeadAnimationsEnum.HeadHighResDamage);
                _parent.OnHeadCollision();
                AudioHandler.PlayAudio(_audioSource);
            }
        }
    }
}
