using UnityEngine;

namespace GameObjects.InteractingObjects.Contraptions.Fan
{
    public class Fan : MonoBehaviour
    {
        public void Update()
        {
            transform.Rotate(0, 100, 1);
        }
    }
}
