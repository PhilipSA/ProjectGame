using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(AudioSource), true)]
    public class uPrefabAudioSourceProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("rolloffFactor");
                names.Add("minVolume");
                names.Add("maxVolume");

                return names;
            }
        }

    }
}