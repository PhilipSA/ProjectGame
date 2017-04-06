using Engine.Audio;
using Enums;
using UnityEngine;

namespace GameObjects.InteractingObjects.Player
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
            _player.PlayerRigidbody2D.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }

        public void OnPaddingCollision(Collision2D collision2D)
        {
            _player.PlayerHitpoints.InflictDamage(5);
            _player.DeadCheck();
        }

        public void OnBouncyCollision(Collision2D collision2D, float force)
        {
            if (collision2D.gameObject.name == "PlayerHead")
            {
                _player.PlayerHitpoints.InflictDamage(10);
                _player.DeadCheck();
            }
        }

        public void OnIceCollision(Collision2D collision2D)
        {
            _player.PlayerRigidbody2D.AddForce(new Vector2(), ForceMode2D.Force);
        }

        public void OnHeadCollision(Collision2D collision2D)
        {
            if (collision2D.gameObject.CompareTag(TagsEnum.IgnoreHeadCollision)) return;
            if (collision2D.gameObject.CompareTag("Player")) return;

            //_player.PlayerHead.HeadRigidbody2D.AddForceAtPosition(-_player.PlayerHead.HeadRigidbody2D.velocity * 20, _player.PlayerHead.HeadRigidbody2D.centerOfMass, ForceMode2D.Impulse);
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
            _player.PlayerControl.MovePlayerOnBounce();
        }
    }
}
