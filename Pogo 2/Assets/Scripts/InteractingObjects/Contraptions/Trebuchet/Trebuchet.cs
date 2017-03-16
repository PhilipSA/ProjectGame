using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions.Trebuchet
{
    public class Trebuchet : MonoBehaviour
    {
        public Counterweigh Counterweigh;
        public Sling Sling;

        public void Awake()
        {
            Counterweigh = GetComponentInChildren<Counterweigh>();
            Sling = GetComponentInChildren<Sling>();
        }

        public void FIRREEEE()
        {
            Counterweigh.AddMass(3800);
        }
    }
}
