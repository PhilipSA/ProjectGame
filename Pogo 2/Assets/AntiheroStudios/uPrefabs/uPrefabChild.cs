using UnityEngine;
using System.Collections.Generic;

namespace AntiheroStudios.uPrefabs
{
    [ExecuteInEditMode()]
    public class uPrefabChild : uObject
    {
        public override uObject asset
        {
            get
            {
#if UNITY_EDITOR
                if (uPrefabParent && uPrefabParent.asset)
                {
                    return FindChild(this.instanceGUID, uPrefabParent.asset as uPrefab);
                }
#endif
                return null;
            }
        }

        public override uPrefab uPrefabParent
        {
            get
            {
                return FindParentComponent<uPrefab>(transform);
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying)
            {
                if (GetComponent<uPrefab>() || !uPrefabParent)
                {
                    Object.DestroyImmediate(this);
                    return;
                }
            }
        }
#endif

        public override void OnProcessChildren()
        {
            base.OnProcessChildren();

            if (uPrefabParent)
            {
                foreach (Transform child in transform)
                {
                    var uChild = (child.GetComponent<uObject>()) ? child.GetComponent<uObject>() : child.gameObject.AddComponent<uPrefabChild>();
                    uChild.parentAssetGUID = this.parentAssetGUID;
                    uPrefabParent.AllocateInstanceId(uChild);
                    uChild.OnProcessChildren();
                }
            }
        }
    }
}