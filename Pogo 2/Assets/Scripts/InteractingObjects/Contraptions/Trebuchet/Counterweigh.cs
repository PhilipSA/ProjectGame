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

        public void AddMass(float mass)
        {
            Rigidbody2D.mass = mass;
        }

        public void ResetMass()
        {
            Rigidbody2D.mass = _startMass;
        }
    }
}
