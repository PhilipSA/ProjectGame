using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player.Parts
{
    public class PlayerFoot : MonoBehaviour
    {
        private Player _parent;
        public PolygonCollider2D Collider2D { get; set; }
        private AudioSource audioSource;

        void OnEnable()
        {
            Collider2D = GetComponent<PolygonCollider2D>();
            _parent = (Player)GetComponentInParent(typeof(Player));
            audioSource = GetComponent<AudioSource>();
            Physics2D.IgnoreCollision(Collider2D, _parent.BoxCollider2D);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (_parent.enabled && col.gameObject != _parent.gameObject)
            {
                _parent.OnFootCollision();
                AudioHandler.PlayAudio(audioSource);
            }
        }
    }
}
