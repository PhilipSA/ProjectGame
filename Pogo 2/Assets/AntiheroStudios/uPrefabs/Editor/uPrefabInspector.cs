using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [CustomEditor(typeof(uPrefab))]
    [CanEditMultipleObjects]
    public class uPrefabInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var prefab = target as uPrefab;

            if (!prefab.isAsset)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
               
                GameObject prefabAssetGameObject = null;

                if (prefab.asset != null)
                {
                    prefabAssetGameObject = prefab.asset.gameObject;
                }

                GameObject assignedPrefabAssetGameObject = (GameObject)EditorGUILayout.ObjectField(prefabAssetGameObject, typeof(GameObject), false);

                if (assignedPrefabAssetGameObject != null)
                {
                    // If we updated to a different prefab reference...
                    if (assignedPrefabAssetGameObject.GetComponent<uPrefab>() && assignedPrefabAssetGameObject != prefabAssetGameObject)
                    {
                        if (EditorUtility.DisplayDialog("Are you sure?", "This will change the prefab asset this instance connects to!", "Change Asset", "Cancel"))
                        {
                            foreach (var selectedTarget in targets)
                            {
                                var targetPrefab = selectedTarget as uPrefab;
                                targetPrefab.assetGUID = assignedPrefabAssetGameObject.GetComponent<uPrefab>().assetGUID;
                            }
                        }
                    }
                }

                if (prefab.instanceGUID != uObject.kNullInstanceGUID)
                {
                    GUI.skin.label.richText = true;
                    GUILayout.Label(string.Format("<b>Instance Id: </b>{0}", prefab.instanceGUID));
                }

                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.skin.label.richText = true;

                if (prefab.transform.parent != null && prefab.instanceGUID != uObject.kNullInstanceGUID)
                {
                    GameObject prefabAssetGameObject = null;

                    if (prefab.asset != null)
                    {
                        prefabAssetGameObject = prefab.asset.gameObject;
                    }

                    EditorGUILayout.ObjectField(prefabAssetGameObject, typeof(GameObject), false);

                    GUILayout.Label(string.Format("<b>Instance Id: </b>{0}", prefab.instanceGUID));
                }
                else
                {
                    GUILayout.Label(string.Format("<b>Asset Id: </b>{0}", prefab.assetGUID));
                }

                GUILayout.EndHorizontal();
            }

        }
    }
}
