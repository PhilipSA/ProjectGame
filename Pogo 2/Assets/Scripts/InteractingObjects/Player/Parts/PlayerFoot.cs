using UnityEngine;

namespace InteractingObjects.Player.Parts
{
    public class PlayerFoot : MonoBehaviour
    {
        private Player _parent;
        public CircleCollider2D Collider2D { get; set; }
        public AudioSource AudioSource;
        public Rigidbody2D Rigidbody2D;

        void OnEnable()
        {
            Collider2D = GetComponent<CircleCollider2D>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _parent = (Player)GetComponentInParent(typeof(Player));
            AudioSource = GetComponent<AudioSource>();
            Physics2D.IgnoreCollision(Collider2D, _parent.BoxCollider2D);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (_parent.enabled && col.gameObject != _parent.gameObject)
            {
                _parent.PlayerCollider.OnFootCollision(col);
            }
        }
    }
}
