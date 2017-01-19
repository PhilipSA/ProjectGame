using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntiheroStudios.uPrefabs
{
    /// <summary>
    /// A component that tracks it's original asset
    /// and allows per-component reverting and applying.
    /// </summary>
    [ExecuteInEditMode]
    public class uPrefab : uObject
    {
        private uObject m_asset;

        /// <summary>
        /// EDIT MODE ONLY: Returns the prefab 
        /// asset that this prefab points to.
        /// </summary>
        public override uObject asset
        {
            get
            {
#if UNITY_EDITOR
                if (m_asset)
                {
                    if (m_asset.assetGUID != this.assetGUID)
                    {
                        m_asset = null;
                    }
                }

                if (!m_asset)
                {
                    string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(this.assetGUID);
                    GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                    if (prefab)
                    {
                        m_asset = prefab.GetComponent<uPrefab>();
                    }
                }
#endif
                return m_asset;
            }
        }

        public UnityEngine.Object containingPrefab
        {
            get
            {
#if UNITY_EDITOR
                if (this.asset)
                {
                    return UnityEditor.PrefabUtility.FindPrefabRoot(this.asset.gameObject);
                }
                else
                {
                    Debug.LogWarningFormat("uPrefab: The instance of uPrefab ({0}) does not have a matching asset!", transform.name);

                    return null;
                }
#else
                return null;
#endif
            }
        }

        public override void OnProcessChildren()
        {
            base.OnProcessChildren();

            foreach (Transform child in transform)
            {
                var uChild = (child.GetComponent<uObject>()) ? child.GetComponent<uObject>() : child.gameObject.AddComponent<uPrefabChild>();
                uChild.parentAssetGUID = this.assetGUID;
                this.AllocateInstanceId(uChild);
                uChild.OnProcessChildren();
            }
        }
    } 
}