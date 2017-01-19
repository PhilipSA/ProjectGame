using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(Animator))]
    public class uPrefabAnimatorProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("playbackTime");
                names.Add("bodyPosition");
                names.Add("bodyRotation");
                names.Add("feetPivotActive");
                names.Add("time");
                names.Add("rootPosition");
                names.Add("rootRotation");

                return names;
            }
        }
    }
}