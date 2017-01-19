using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;
using System.Collections.Generic;

namespace AntiheroStudios.uPrefabs.Editor
{
    public class uPrefabRevertUtilities
    {
        private static void RevertPrefabTransform(GameObject original, GameObject instance)
        {
            instance.transform.localScale = original.transform.localScale;

            if (instance.GetComponent<RectTransform>() && original.GetComponent<RectTransform>())
            {
                RectTransform originalRect = original.GetComponent<RectTransform>();
                RectTransform instanceRect = instance.GetComponent<RectTransform>();

                instanceRect.offsetMin = originalRect.offsetMin;
                instanceRect.offsetMax = originalRect.offsetMax;
                instanceRect.anchorMin = originalRect.anchorMin;
                instanceRect.anchorMax = originalRect.anchorMax;
                instanceRect.pivot = originalRect.pivot;
            }
        }

        /// <summary>
        /// Safely reverts the transform, keeping instance properties in-tact.
        /// </summary>
        /// <param name="original">Original.</param>
        /// <param name="instance">Instance.</param>
        private static void RevertChildTransform(GameObject original, GameObject instance)
        {
            instance.transform.name = original.transform.name;
            instance.transform.SetSiblingIndex(original.transform.GetSiblingIndex());
            instance.transform.localPosition = original.transform.localPosition;
            instance.transform.localRotation = original.transform.localRotation;
            instance.transform.localScale = original.transform.localScale;

            if (instance.GetComponent<RectTransform>() && original.GetComponent<RectTransform>())
            {
                RectTransform originalRect = original.GetComponent<RectTransform>();
                RectTransform instanceRect = instance.GetComponent<RectTransform>();

                instanceRect.offsetMin = originalRect.offsetMin;
                instanceRect.offsetMax = originalRect.offsetMax;
                instanceRect.anchorMin = originalRect.anchorMin;
                instanceRect.anchorMax = originalRect.anchorMax;
                instanceRect.pivot = originalRect.pivot;
                instanceRect.anchoredPosition3D = originalRect.anchoredPosition3D;
            }
        }

        public static void RevertAllOnObject(uObject instance)
        {
            if (instance.asset != null)
            {
                instance.gameObject.SetActive(instance.asset.gameObject.activeSelf);
                instance.gameObject.layer = instance.asset.gameObject.layer;
                instance.gameObject.tag = instance.asset.gameObject.tag;

                RevertPrefabTransform(instance.asset.gameObject, instance.gameObject);

                // Revert children...
                OnRevertAllChildren(instance);

                // Revert components...
                RevertObjectComponents(instance);
            }
        }

        private static void OnRevertAllChildren(uObject instance)
        {
            uObject searchableInstanceParent = instance;
            uObject searchableAssetParent = instance.asset;

            if (searchableInstanceParent.GetComponent<uPrefabChild>())
            {
                searchableInstanceParent = instance.uPrefabParent;
                searchableAssetParent = instance.asset.uPrefabParent;
            }

            if (instance.asset != null)
            {
                // Check for missing...
                foreach (Transform assetChildTransform in instance.asset.transform)
                {
                    uObject assetChild = assetChildTransform.GetComponent<uObject>();

                    if (assetChildTransform.parent != instance.asset.transform)
                    {
                        continue;
                    }

                    uObject foundChild = uObject.FindChild(assetChild.instanceGUID, searchableInstanceParent);

                    if (!foundChild)
                    {
                        var createdInstance = Object.Instantiate(assetChildTransform.gameObject);
                        createdInstance.transform.SetParent(instance.transform, true);
                        createdInstance.GetComponent<uObject>().instanceGUID = assetChildTransform.GetComponent<uObject>().instanceGUID;

                        RevertChildTransform(assetChildTransform.gameObject, createdInstance);
                    }
                    else
                    {
                        // Check for correct parentage...
                        uObject assetChildParent = uObject.FindParentComponent<uObject>(assetChild.transform);
                        uObject instanceChildParent = uObject.FindParentComponent<uObject>(foundChild.transform);


                        // If we are not a child of our uPrefab parent...
                        if (assetChildParent != searchableAssetParent)
                        {
                            if (assetChildParent.instanceGUID != instanceChildParent.instanceGUID)
                            {
                                uObject correctFoundChildParent = uObject.FindChild(assetChildParent.instanceGUID, searchableInstanceParent);

                                if (correctFoundChildParent)
                                {
                                    foundChild.transform.SetParent(correctFoundChildParent.transform);
                                }
                            }
                        }
                        else
                        {
                            // If we are not the child of the uPrefab parent...
                            if (instanceChildParent != searchableInstanceParent)
                            {
                                foundChild.transform.SetParent(searchableInstanceParent.transform);
                            }
                        }

                        RevertChildTransform(assetChildTransform.gameObject, foundChild.gameObject);
                        RevertAllOnObject(foundChild);

                        // TODO: this is hacky... we need to find a good way to handle instance inside child, vs. instance of self.
                        foundChild.gameObject.SetActive(assetChildTransform.gameObject.activeSelf);
                    }
                }
                
                // Check for added...
                for (int i = instance.transform.childCount - 1; i >= 0; i--)
                {
                    Transform instanceChildTransform = instance.transform.GetChild(i);

                    uObject foundChild = uObject.FindChild(instanceChildTransform.GetComponent<uObject>().instanceGUID, searchableAssetParent);

                    if (!foundChild)
                    {
                        Undo.RecordObject(instanceChildTransform.gameObject, "Undo Revert Object");

                        Object.DestroyImmediate(instanceChildTransform.gameObject);
                    }
                }
            }
        }

        public static void RevertTransformOnAllObjectInstances(uObject asset, bool revertPosition, bool revertRotation, bool revertScale, bool revertAnchorPosition, bool revertOffset, bool revertPivot, bool revertAnchors)
        {
            GameObject[] allGameObjects = uPrefabUtility.FindAllPrefabObjects().ToArray();

            for (int i = allGameObjects.Length - 1; i >= 0; i--)
            {
                var targetPrefab = allGameObjects[i];
                var targetPrefabAsset = targetPrefab.GetComponent<uPrefab>();

                if (!targetPrefabAsset)
                {
                    continue;
                }

                if (asset.GetComponent<uPrefab>())
                {
                    // Same prefab, skip...
                    if (asset.assetGUID == targetPrefabAsset.assetGUID)
                    {
                        continue;
                    }
                }

                uObject[] childObjects = targetPrefab.GetComponentsInChildren<uObject>(true);

                for (int j = childObjects.Length - 1; j >= 0; j--)
                {
                    if (asset.GetComponent<uPrefab>())
                    {
                        if (!childObjects[j].GetComponent<uPrefab>())
                        {
                            continue;
                        }

                        // Not the same prefab...
                        if (asset.assetGUID != childObjects[j].assetGUID)
                        {
                            continue;
                        }
                    }

                    if (asset.GetComponent<uPrefabChild>())
                    {
                        if (!childObjects[j].GetComponent<uPrefabChild>())
                        {
                            continue;
                        }

                        // Not the same child...
                        if (asset.instanceGUID != childObjects[j].instanceGUID || asset.parentAssetGUID != childObjects[j].parentAssetGUID)
                        {
                            continue;
                        }
                    }

                    Undo.RecordObject(childObjects[j].transform, "Revert Transform");

                    if (revertPosition)
                    {
                        childObjects[j].transform.localPosition = asset.transform.position;
                    }

                    if (revertRotation)
                    {
                        childObjects[j].transform.rotation = asset.transform.rotation;
                    }

                    if (revertScale)
                    {
                        childObjects[j].transform.localScale = asset.transform.lossyScale;
                    }

                    if (revertAnchorPosition)
                    {
                        childObjects[j].GetComponent<RectTransform>().anchoredPosition3D = asset.GetComponent<RectTransform>().anchoredPosition3D;
                    }

                    if (revertOffset)
                    {
                        childObjects[j].GetComponent<RectTransform>().offsetMin = asset.GetComponent<RectTransform>().offsetMin;
                        childObjects[j].GetComponent<RectTransform>().offsetMax = asset.GetComponent<RectTransform>().offsetMax;
                    }

                    if (revertPivot)
                    {
                        childObjects[j].GetComponent<RectTransform>().pivot = asset.GetComponent<RectTransform>().pivot;
                    }

                    if (revertAnchors)
                    {
                        childObjects[j].GetComponent<RectTransform>().anchorMin = asset.GetComponent<RectTransform>().anchorMin;
                        childObjects[j].GetComponent<RectTransform>().anchorMax = asset.GetComponent<RectTransform>().anchorMax;
                    }

                }
            }
        }

        public static void RevertObjectComponents(uObject instance)
        {
            // Revert instance components...
            Component[] components = instance.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (!components[i] || typeof(uObject).IsAssignableFrom(components[i].GetType()) || typeof(Transform).IsAssignableFrom(components[i].GetType()))
                {
                    continue;
                }

                Component instanceComponent = components[i];
                Component assetComponent = instance.asset.GetComponent(instanceComponent.GetType());

                if (assetComponent != null)
                {
                    uPrefabUtility.RevertComponentWithProcessor(instance.asset.gameObject, instance.gameObject, assetComponent, instanceComponent);
                }
                else
                {
                    Object.DestroyImmediate(instanceComponent);
                }
            }

            // Check for missing components...
            Component[] assetComponents = instance.asset.GetComponents<Component>();

            for (int i = 0; i < assetComponents.Length; i++)
            {
                if (!assetComponents[i]) { continue; }
                if (typeof(uObject).IsAssignableFrom(assetComponents[i].GetType()) || typeof(Transform).IsAssignableFrom(assetComponents[i].GetType()))
                {
                    continue;
                }

                Component instanceComponent = (instance.GetComponent(assetComponents[i].GetType())) ? instance.GetComponent(assetComponents[i].GetType()) : instance.gameObject.AddComponent(assetComponents[i].GetType());
                uPrefabUtility.RevertComponentWithProcessor(instance.asset.gameObject,  instance.gameObject, assetComponents[i], instanceComponent);
            }
        }

        public static void RevertAllObjectInstanceComponents(uObject asset)
        {
            Component[] assetComponents = asset.GetComponents<Component>();
            GameObject[] allGameObjects = uPrefabUtility.FindAllPrefabObjects().ToArray();

            foreach (var assetComponent in assetComponents)
            {
                if (typeof(uObject).IsAssignableFrom(assetComponent.GetType()) || typeof(Transform).IsAssignableFrom(assetComponent.GetType()))
                {
                    continue;
                }

                for (int i = allGameObjects.Length - 1; i >= 0; i--)
                {
                    var targetPrefab = allGameObjects[i];
                    var targetPrefabAsset = targetPrefab.GetComponent<uPrefab>();

                    float progress = i / allGameObjects.Length;

                    EditorUtility.DisplayProgressBar("Replacing Components...", "Targeting Prefab: " + targetPrefab.name, 1f - progress);

                    if (asset.unityAsset == targetPrefab)
                    {
                        continue;
                    }

                    if (targetPrefabAsset != null && targetPrefabAsset.assetGUID != asset.assetGUID)
                    {
                        var targetPrefabInstance = targetPrefab;

                        uObject[] childPrefabs = targetPrefabInstance.GetComponentsInChildren<uObject>(true);

                        for (int j = childPrefabs.Length - 1; j >= 0; j--)
                        {
                            bool doRevert = false;

                            if (childPrefabs[j].GetComponent<uPrefab>() && asset.GetComponent<uPrefab>())
                            {
                                if (childPrefabs[j].assetGUID == asset.assetGUID)
                                {
                                    doRevert = true;
                                }
                            }

                            if (childPrefabs[j].GetComponent<uPrefabChild>() && asset.GetComponent<uPrefabChild>())
                            {
                                if (((uPrefabChild)childPrefabs[j]).parentAssetGUID == ((uPrefabChild)asset).parentAssetGUID && childPrefabs[j].instanceGUID == asset.instanceGUID)
                                {
                                    doRevert = true;
                                }
                            }

                            if (doRevert)
                            {
                                var childInstance = childPrefabs[j];
                                var instanceComponent = (childInstance.GetComponent(assetComponent.GetType())) ? childInstance.GetComponent(assetComponent.GetType()) : childInstance.gameObject.AddComponent(assetComponent.GetType());

                                Undo.RecordObject(instanceComponent, "Revert Component");

                                uPrefabUtility.RevertComponentWithProcessor(asset.gameObject, childInstance.gameObject, assetComponent, instanceComponent);
                            }
                        }
                    }
                }
            }


            SceneView.RepaintAll();

            EditorUtility.ClearProgressBar();
        }

        public static void RevertAllObjectInstances(uObject asset)
        {
            GameObject[] allGameObjects = uPrefabUtility.FindAllPrefabObjects().ToArray();

            for (int j = allGameObjects.Length - 1; j >= 0; j--)
            {
                var targetPrefab = allGameObjects[j];
                var targetPrefabAsset = targetPrefab.GetComponent<uPrefab>();

                float progress = j / allGameObjects.Length;

                EditorUtility.DisplayProgressBar("Replacing Prefab Children...", "Targeting: " + targetPrefab.name, 1f - progress);

                if (asset.unityAsset == targetPrefab)
                {
                    continue;
                }

                // Only uPrefabs...
                if (targetPrefabAsset && targetPrefabAsset.assetGUID != asset.assetGUID)
                {
                    var targetPrefabInstance = Object.Instantiate(targetPrefabAsset.containingPrefab) as GameObject;

                    uObject[] childObjects = targetPrefabInstance.GetComponentsInChildren<uObject>(true);

                    bool foundChildToReplace = false;

                    for (int i = childObjects.Length - 1; i >= 0; i--)
                    {
                        bool doRevert = false;

                        if (asset.GetComponent<uPrefab>())
                        {
                            if (childObjects[i].assetGUID == asset.assetGUID)
                            {
                                doRevert = true;
                            }
                        }

                        if (asset.GetComponent<uPrefabChild>())
                        {
                            if (childObjects[i].parentAssetGUID == asset.parentAssetGUID && childObjects[i].instanceGUID == asset.instanceGUID)
                            {
                                doRevert = true;
                            }
                        }

                        // If we find a prefab that matches this asset...
                        if (doRevert)
                        {
                            RevertAllOnObject(childObjects[i]);

                            foundChildToReplace = true;
                        }
                    }

                    if (foundChildToReplace)
                    {
                        uPrefabApplyUtilities.ApplySelf(targetPrefabInstance.GetComponent<uPrefab>(), ReplacePrefabOptions.ReplaceNameBased);
                    }

                    // Remove temp object for saving...
                    Object.DestroyImmediate(targetPrefabInstance);
                }


                Resources.UnloadUnusedAssets();
            }

            SceneView.RepaintAll();

            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// Reverts the component on prefab child instances.
        /// </summary>
        /// <param name="parentAsset">The parent asset.</param>
        /// <param name="refChild">The reference child.</param>
        /// <param name="refComponent">The reference component.</param>
        public static void RevertComponentOnAllObjectInstances(uObject targetObj, Component refComponent)
        {
            GameObject[] allGameObjects = uPrefabUtility.FindAllPrefabObjects().ToArray();

            for (int i = allGameObjects.Length - 1; i >= 0; i--)
            {
                var targetPrefab = allGameObjects[i];
                var targetPrefabAsset = targetPrefab.GetComponent<uPrefab>();

                float progress = i / allGameObjects.Length;

                EditorUtility.DisplayProgressBar("Replacing Components...", "Targeting Prefab: " + targetPrefab.name, 1f - progress);

                // Skip reverting on self...
                if (targetObj.unityAsset == targetPrefab)
                {
                    continue;
                }

                if (targetPrefabAsset)
                {
                    var targetPrefabInstance = targetPrefab;

                    uObject[] childPrefabs = targetPrefabInstance.GetComponentsInChildren<uObject>(true);

                    for (int j = childPrefabs.Length - 1; j >= 0; j--)
                    {
                        bool doRevert = false;

                        if (targetObj.GetComponent<uPrefab>())
                        {
                            if (childPrefabs[j].assetGUID == targetObj.assetGUID)
                            {
                                doRevert = true;
                            }
                        }

                        if (targetObj.GetComponent<uPrefabChild>())
                        {
                            if (childPrefabs[j].instanceGUID == targetObj.instanceGUID && childPrefabs[j].parentAssetGUID == targetObj.parentAssetGUID)
                            {
                                doRevert = true;
                            }
                        }

                        if (doRevert)
                        {
                            var childInstance = childPrefabs[j];
                            var instanceComponent = (childInstance.GetComponent(refComponent.GetType())) ? childInstance.GetComponent(refComponent.GetType()) : childInstance.gameObject.AddComponent(refComponent.GetType());

                            Undo.RecordObject(instanceComponent, "Revert Component");

                            uPrefabUtility.RevertComponentWithProcessor(targetObj.gameObject, childInstance.gameObject,  refComponent, instanceComponent);
                        }
                    }
                }
            }

            SceneView.RepaintAll();

            EditorUtility.ClearProgressBar();
        }

    }
}