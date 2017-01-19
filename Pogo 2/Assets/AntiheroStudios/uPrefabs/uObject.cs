using UnityEngine;
using System.Collections.Generic;

namespace AntiheroStudios.uPrefabs
{
    public class uObject : MonoBehaviour
    {
        public const long kNullInstanceGUID = 0;

        public Component[] modifiedComponents;
        public Component[] addedComponents;
        public Component[] removedComponents;
        public bool childrenChanged;

        public string assetGUID;
        public string parentAssetGUID;
        public long instanceGUID;

        public List<long> childrenIndicies = new List<long>();
        public List<uObject> children = new List<uObject>();

        private long m_allocatedInstanceGUID = 0;

        public virtual bool isAsset
        {
            get
            {
#if UNITY_EDITOR
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(gameObject);

                return !string.IsNullOrEmpty(assetPath);                                                     
#else
                return false;
#endif
            }
        }

        public virtual uPrefab uPrefabParent
        {
            get
            {
#if UNITY_EDITOR
                if (isAsset)
                {
                    string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(parentAssetGUID);

                    return UnityEditor.AssetDatabase.LoadAssetAtPath<uPrefab>(assetPath);
                }
                else
                {
                    return FindParentComponent<uPrefab>(transform);
                }
#else
                return null;
#endif
            }
        }

        public virtual uObject asset
        {
            get
            {
                return null;
            }
        }

        public virtual UnityEngine.Object unityAsset
        {
            get
            {
#if UNITY_EDITOR
                if (isAsset)
                {
                    return UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GetAssetPath(gameObject), typeof(GameObject));
                }

                return null;
#else

                return null;
#endif
            }

        }

        protected virtual void OnTransformChildrenChanged()
        {
            this.OnProcessChildren();
        }

        public virtual void OnProcessChildren()
        {

        }

        public virtual void AllocateInstanceId(uObject child)
        {
            ClearNullChildren();

            if (child.instanceGUID == kNullInstanceGUID)
            {
                RegisterChild(child);
            }
            else
            {
                int i = childrenIndicies.IndexOf(child.instanceGUID);

                // Found an index...
                if (i != -1)
                {
                    // Duplicate key...
                    if (children[i] != child)
                    {
                        RegisterChild(child);
                    }
                }
                else
                {
                    RegisterChild(child, true);
                }
            }
        }

        private void ClearNullChildren()
        {
            for (int i = childrenIndicies.Count - 1; i >= 0; i--)
            {
                if (children[i] == null)
                {
                    children.RemoveAt(i);
                    childrenIndicies.RemoveAt(i);
                }
            }
        }

        private void RegisterChild(uObject child, bool keepInstanceGUID = false)
        {
            long assigedInstanceGUID = child.instanceGUID;

            if (!keepInstanceGUID)
            {
                while (childrenIndicies.Contains(m_allocatedInstanceGUID))
                {
                    m_allocatedInstanceGUID++;
                }

                assigedInstanceGUID = m_allocatedInstanceGUID;
            }

            child.instanceGUID = assigedInstanceGUID;
            childrenIndicies.Add(assigedInstanceGUID);
            children.Add(child);
        }

        #region Static Methods
        public static T FindParentComponent<T>(Transform child) where T : Component
        {
            Transform parent = child.parent;

            while (parent != null)
            {
                if (parent.GetComponent<T>())
                {
                    return parent.GetComponent<T>();
                }
                else
                {
                    parent = parent.parent;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Finds the child of a prefab regardless of the depth.
        /// </summary>
        /// <param name="instanceId">The asset unique identifier.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public static uObject FindChild(long instanceId, uObject parent)
        {
            if (parent != null && instanceId != kNullInstanceGUID)
            {
                if (parent.childrenIndicies.IndexOf(instanceId) != -1)
                {
                    return parent.children[parent.childrenIndicies.IndexOf(instanceId)];
                }
            }

            return null;
        }

        #endregion
    }
}