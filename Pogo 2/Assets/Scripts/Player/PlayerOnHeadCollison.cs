using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerOnHeadCollison : MonoBehaviour
    {
        private GameObject parent;
        private Rigidbody2D headRigidbody2D;

        void Start()
        {
            headRigidbody2D = GetComponent<Rigidbody2D>();
            headRigidbody2D.freezeRotation = true;
            parent = transform.parent.gameObject;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            parent.SendMessage("OnHeadCollision");
        }
    }
}
