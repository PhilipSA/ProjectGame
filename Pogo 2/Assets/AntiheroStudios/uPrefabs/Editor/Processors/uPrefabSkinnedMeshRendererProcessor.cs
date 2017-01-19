using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(SkinnedMeshRenderer), true)]
    public class uPrefabSkinnedMeshRendererProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("rootBone");
                names.Add("actualRootBone");
                names.Add("bones");

                return names;
            }
        }

    }
}