using UnityEngine;

namespace GameObjects.InteractingObjects.Player.Parts
{
    public class PlayerFoot : MonoBehaviour
    {
        private GameObjects.InteractingObjects.Player.Player _parent;
        public CircleCollider2D Collider2D { get; set; }
        public AudioSource AudioSource;
        public Rigidbody2D Rigidbody2D;

        void OnEnable()
        {
            Collider2D = GetComponent<CircleCollider2D>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _parent = (GameObjects.InteractingObjects.Player.Player)GetComponentInParent(typeof(GameObjects.InteractingObjects.Player.Player));
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
