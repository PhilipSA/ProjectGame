using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions.Trebuchet
{
    public class Counterweigh : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;

        void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            
        }

        public void AddMass(float mass)
        {
            Rigidbody2D.mass = mass;
        }
    }
}
