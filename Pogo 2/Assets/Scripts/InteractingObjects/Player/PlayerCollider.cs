using Assets.Scripts.Engine.Audio;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerCollider
    {
        private readonly Player _player;

        public PlayerCollider(Player player)
        {
            _player = player;
        }

        public void OnTrampolineCollision(Collision2D collision2D, float force)
        {
            _player.PlayerRigidbody2D.velocity = new Vector2(_player.PlayerRigidbody2D.velocity.x, _player.PlayerRigidbody2D.velocity.y + force);
        }

        public void OnPaddingCollision(Collision2D collision2D)
        {
            _player.PlayerHitpoints.InflictDamage(5);
            _player.DeadCheck();
        }

        public void OnIceCollision(Collision2D collision2D)
        {
            //_player.AnglePlayer();
            _player.PlayerRigidbody2D.AddForce(new Vector2(), ForceMode2D.Force);
        }

        public void OnHeadCollision(Collision2D collision2D)
        {
            if (collision2D.gameObject.CompareTag(TagsEnum.IgnoreHeadCollision)) return;
            if (collision2D.gameObject.CompareTag("Player")) return;

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
            if (collision2D.gameObject.CompareTag(TagsEnum.IgnoreFootCollision)) return;

            AudioHandler.PlayAudio(_player.PlayerFoot.AudioSource);
            _player.AnglePlayer();
            _player.MovePlayerOnBounce();
        }
    }
}
