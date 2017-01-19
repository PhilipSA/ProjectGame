using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    /// <summary>
    /// Handles the processing of the uPrefab object.
    /// </summary>
    class uPrefabAssetPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var importedAsset in importedAssets)
            {
                if (importedAsset.Contains(".prefab"))
                {
                    EditorUtility.DisplayProgressBar("Importing Prefab", "Please wait while we import the prefab: " + importedAsset, .5f);

                    GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(importedAsset);

                    if (asset)
                    {
                        var uPrefab = (asset.GetComponent<uPrefab>()) ? asset.GetComponent<uPrefab>() : asset.AddComponent<uPrefab>();
                        uPrefab.assetGUID = AssetDatabase.AssetPathToGUID(importedAsset);
                        uPrefab.OnProcessChildren();

                        if (asset.GetComponent<uPrefabChild>())
                        {
                            Object.DestroyImmediate(asset.GetComponent<uPrefabChild>(), true);
                        }
                    }

                    EditorUtility.ClearProgressBar();
                }
            }
        }
    }
}
