using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(AudioListener), true)]
    public class uPrefabAudioListenerProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("velocityUpdateMode");

                return names;
            }
        }

    }
}