using Assets.Scripts.Engine;
using Assets.Scripts.GameObjects.Components.Animation;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Hazards
{
    public class SawBlade : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteAnimation _animationHandler;

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
