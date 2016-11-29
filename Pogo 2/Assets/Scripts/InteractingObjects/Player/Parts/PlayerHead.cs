using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player.Parts
{
    public class PlayerHead : MonoBehaviour
    {
        private Player _parent;
        private Rigidbody2D _headRigidbody2D;
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _headRigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _headRigidbody2D.freezeRotation = true;
            _parent = transform.parent.GetComponent<Player>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (_parent.enabled)
            {
                _parent.OnHeadCollision();
                _spriteRenderer.color = Color.red;
            }
        }
    }
}
