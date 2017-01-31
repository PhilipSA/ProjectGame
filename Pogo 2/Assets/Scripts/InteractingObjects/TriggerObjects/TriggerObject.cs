using UnityEngine;

namespace Assets.Scripts.InteractingObjects.TriggerObjects
{
    public abstract class TriggerObject : MonoBehaviour
    {
        public BoxCollider2D TriggerCollider2D { get; private set; }

        public virtual void Awake()
        {
            TriggerCollider2D = GetComponent<BoxCollider2D>();
        }

        protected abstract void OnTriggerEnter2D(Collider2D other);
    }
}
