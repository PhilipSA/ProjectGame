using UnityEngine;

namespace Assets.Scripts.InteractingObjects.TriggerObjects
{
    public abstract class TriggerObject : MonoBehaviour
    {
        public BoxCollider2D TriggerCollider2D { get; private set; }

        void Awake()
        {
            TriggerCollider2D = GetComponent<BoxCollider2D>();
        }
    }
}
