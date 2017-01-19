using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace AntiheroStudios.uPrefabs
{
    public class uPrefabApplyUtilities
    {
        /// <summary>
        /// Applies each parent uPrefab parent until we passed the root.
        /// </summary>
        /// <param name="instance">Instance.</param>
        public static void ApplyUp(uPrefab instance, ReplacePrefabOptions options = ReplacePrefabOptions.ConnectToPrefab)
        {
            if (instance)
            {
                // Apply each parent level hierarchy up...
                Transform parentTransform = instance.gameObject.transform.parent;

                while (parentTransform != null)
                {
                    var parentUPrefab = parentTransform.GetComponent<uPrefab>();

                    if (parentUPrefab)
                    {
                        // Does this have a uPrefab parent?
                        if (parentUPrefab.uPrefabParent)
                        {
                            PrefabUtility.ReplacePrefab(parentTransform.gameObject, parentUPrefab.asset.gameObject, ReplacePrefabOptions.Default);
                        }
                        else
                        {
                            PrefabUtility.ReplacePrefab(parentTransform.gameObject, parentUPrefab.asset.gameObject, ReplacePrefabOptions.ConnectToPrefab);
                        }
                    }

                    parentTransform = parentTransform.transform.parent;
                }
            }
        }

        public static void ApplySelf(uPrefab instance, ReplacePrefabOptions options = ReplacePrefabOptions.ConnectToPrefab)
        {
            EditorUtility.DisplayProgressBar("Applying Prefab...", "Please wait while we apply the prefab: " + instance.asset.name, .5f);

            PrefabUtility.ReplacePrefab(instance.gameObject, instance.asset.gameObject, options);

            EditorUtility.ClearProgressBar();
        }
    }
}