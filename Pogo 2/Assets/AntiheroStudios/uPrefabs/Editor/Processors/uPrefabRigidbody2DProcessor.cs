using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(Rigidbody2D))]
    public class uPrefabRigidbody2DProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("inertia");
                names.Add("simulated");

                return names;
            }
        }
    }
}