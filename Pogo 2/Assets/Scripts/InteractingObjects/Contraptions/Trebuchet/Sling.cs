using UnityEngine;

namespace InteractingObjects.Contraptions.Trebuchet
{
    public class Sling : MonoBehaviour
    {
        public Trebuchet Parent;

        void Awake()
        {
            Parent = GetComponentInParent<Trebuchet>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.name != "PlayerFoot") return;
            Parent.FIRREEEE();
        }
    }
}
