using UnityEngine;

namespace InteractingObjects.Contraptions.Trebuchet
{
    public class Counterweigh : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        private float _startMass;

        void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _startMass = Rigidbody2D.mass;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            
        }

        public void AddForce(float force)
        {
            Rigidbody2D.AddForce(new Vector2(0, -force), ForceMode2D.Force);
        }

        public void ResetMass()
        {
            Rigidbody2D.mass = _startMass;
        }
    }
}
