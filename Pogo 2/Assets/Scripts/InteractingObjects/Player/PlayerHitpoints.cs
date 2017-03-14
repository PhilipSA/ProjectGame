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

        public void InflictDamage(float damage)
        {
            _hitpoints -= damage;
        }

        public void CalculateImpactDamage(Rigidbody2D rigidbody2D)
        {
            _hitpoints -= rigidbody2D.velocity.magnitude;
        }

        public void InflictHazardDamage()
        {
            _hitpoints -= 20;
        }
    }
}
