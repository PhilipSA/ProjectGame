using Assets.Scripts.Engine.Audio;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerCollider
    {
        private Player _player;

        public PlayerCollider(Player player)
        {
            _player = player;
        }

        public void OnTrampolineCollision(Collision2D collision2D)
        {
            _player.PlayerRigidbody2D.velocity = new Vector2(_player.PlayerRigidbody2D.velocity.x, _player.PlayerRigidbody2D.velocity.y + 200);
        }

        public void OnPaddingCollision(Collision2D collision2D)
        {
            _player.PlayerHitpoints.InflictDamage(-10);
            _player.DeadCheck();
        }

        public void OnHeadCollision(Collision2D collision2D)
        {
            if (collision2D.gameObject.name == "Padding") return;

            AudioHandler.PlayAudio(_player.PlayerHead.AudioSource);
            _player.PlayerHitpoints.CalculateImpactDamage(_player.PlayerRigidbody2D);


            _player.DeadCheck();
        }

        public void OnHazardCollision(Collision2D collision2D)
        {
            _player.PlayerHitpoints.InflictHazardDamage();
            _player.PlayerHead.AnimateDamage();
            _player.DeadCheck();
        }

        public void OnFootCollision(Collision2D collision2D)
        {
            _player.AnglePlayer();
            _player.MovePlayerOnBounce();
        }
    }
}
