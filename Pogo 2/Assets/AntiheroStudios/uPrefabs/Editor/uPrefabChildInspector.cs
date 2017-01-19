using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    /// <exclude />
    [CustomEditor(typeof(uPrefabChild))]
    public class uPrefabChildInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var child = target as uPrefabChild;

            // If the prefab exists...
            if (child.uPrefabParent)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                GUILayout.Label(child.instanceGUID.ToString());
                GUILayout.EndHorizontal();
            }
        }
    }
}
