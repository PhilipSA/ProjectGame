using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerOnFootCollision : MonoBehaviour
    {
        private GameObject parent;

        void Start()
        {
            parent = transform.parent.gameObject;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            parent.SendMessage("OnFootCollision");
        }
    }
}
