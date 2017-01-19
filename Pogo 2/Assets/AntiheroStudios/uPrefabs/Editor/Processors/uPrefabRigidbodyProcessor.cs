using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(Rigidbody))]
    public class uPrefabRigidbodyProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("inertiaTensor");
                names.Add("sleepVelocity");
                names.Add("sleepAngularVelocity");
                names.Add("useConeFriction");
                names.Add("centerOfMass");
                names.Add("position");
                names.Add("rotation");

                return names;
            }
        }
    }
}