#if UNITY_5_5_OR_NEWER
using UnityEngine.AI;
#else
using UnityEngine;
#endif

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(NavMeshAgent), true)]
    public class uPrefabNavMeshAgentProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("path");

                return names;
            }
        }
    }
}