using UnityEngine;

namespace Assets.Scripts.Player.Parts
{
    public class PlayerFoot : MonoBehaviour
    {
        private Player _parent;
        public PolygonCollider2D Collider2D { get; set; }

        void OnEnable()
        {
            Collider2D = GetComponent<PolygonCollider2D>();
            _parent = transform.parent.GetComponent<Player>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (_parent.enabled) _parent.OnFootCollision();
        }
    }
}
