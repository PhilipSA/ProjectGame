using UnityEngine;

namespace InteractingObjects.Player
{
    public class PlayerHitpoints
    {
        private float _hitpoints = 100;
        private readonly Player _player;

        public PlayerHitpoints(Player player)
        {
            _player = player;
        }

        public float Hitpoints
        {
            get { return _hitpoints; }
        }

        public void InflictDamage(float damage)
        {
            _hitpoints -= damage;
            _player.DeadCheck();
        }

        public void CalculateImpactDamage(Rigidbody2D rigidbody2D)
        {
            _hitpoints -= Mathf.Abs(rigidbody2D.velocity.x + rigidbody2D.velocity.y)/2;
            _player.DeadCheck();
        }

        public void InflictHazardDamage()
        {
            _hitpoints -= 20;
            _player.DeadCheck();
        }
    }
}
