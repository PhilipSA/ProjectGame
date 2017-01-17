using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerHitpoints
    {
        private float _hitpoints = 100;

        public float Hitpoints
        {
            get { return _hitpoints; }
        }

        public void CalculateDamage(Rigidbody2D rigidbody2D)
        {
            _hitpoints -= Mathf.Abs(rigidbody2D.velocity.x + rigidbody2D.velocity.y) * 1.4f;
        }


    }
}
