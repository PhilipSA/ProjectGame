using Engine;
using Enums;
using Enums.Player.PlayerHead;
using InteractingObjects.Abstraction;
using UnityEngine;

namespace InteractingObjects.Player.Parts
{
    public class PlayerHead : AnimatedSprite
    {
        public Player Parent;
        public Rigidbody2D HeadRigidbody2D;
        public SpriteRenderer SpriteRenderer;
        public AudioSource AudioSource;

        protected void Awake()
        {
            HeadRigidbody2D = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            AudioSource = GetComponent<AudioSource>();
            Parent = transform.parent.GetComponent<Player>();

            base.Awake("Textures/Player/PlayerHead/");
        }

        public void AnimateDamage()
        {
            SpriteAnimation.AnimateBlink(EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighRes), EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighResDamage),
                PlayerHeadAnimationsEnum.HeadHighResDamage);
        }

        public void AnimateDeath()
        {
            SpriteAnimation.AnimatePermanent(EnumHelper.GetMemberName(() => PlayerHeadAnimationsEnum.HeadHighResDead));
        }

        void Update()
        {
            if (HeadRigidbody2D.velocity.magnitude > 200)
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
            if (Parent.enabled)
            {
                AnimateDamage();
                Parent.PlayerCollider.OnHeadCollision(col);
            }
        }
    }
}
