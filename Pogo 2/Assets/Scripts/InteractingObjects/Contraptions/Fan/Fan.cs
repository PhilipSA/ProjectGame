using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions.Fan
{
    public class Fan : MonoBehaviour
    {
        public void Update()
        {
            transform.Rotate(0, 100, 1);
        }
    }
}
