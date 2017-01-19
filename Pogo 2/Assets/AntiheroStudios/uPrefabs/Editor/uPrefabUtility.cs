using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;

namespace AntiheroStudios.uPrefabs.Editor
{
    /// <summary>
    /// Contains the methods and utilities to render the GUI for the uPrefab
    /// and replace, revert components or the entirety of a uPrefab instance or asset.
    /// </summary>
    public class uPrefabUtility
    {
        public static Dictionary<Type, Dictionary<string, SerializedProperty>> kCachedSerializedProperties = new Dictionary<Type, Dictionary<string, SerializedProperty>>();

        public const float kButtonWidth = 100;
        public const float kIconSize = 24;

        /// <summary>
        /// Wrapper method for replacing the component on a target prefab version.
        /// </summary>
        /// <param name="assetGameObject"></param>
        /// <param name="assetComponent"></param>
        /// <param name="instanceGameObject"></param>
        /// <param name="instanceComponent"></param>
        public static void RevertComponentWithProcessor(GameObject assetGameObject, GameObject instanceGameObject, Component assetComponent, Component instanceComponent)
        {
            var processors = uPrefabComponentProcessor.FindAll(assetComponent.GetType());

            if (processors.Count > 0)
            {
                foreach (var processor in processors)
                {
                    processor.OnRevertComponent(assetComponent.GetType(), assetGameObject, instanceGameObject, assetComponent, instanceComponent);
                }
            }
        }

        /// <summary>
        /// Wrapper method for applying a component on a target prefab.
        /// </summary>
        /// <param name="assetGameObject"></param>
        /// <param name="instanceGameObject"></param>
        /// <param name="assetComponent"></param>
        /// <param name="instanceComponent"></param>
        public static void ApplyComponentWithProcessor(GameObject assetGameObject, GameObject instanceGameObject, Component assetComponent, Component instanceComponent)
        {
            var processors = uPrefabComponentProcessor.FindAll(assetComponent.GetType());

            if (processors.Count > 0)
            {
                foreach (var processor in processors)
                {
                    processor.OnApplyComponent(assetComponent.GetType(), assetGameObject, instanceGameObject, assetComponent, instanceComponent);
                }
            }
        }

        /// <summary>
        /// High level method to find correct reference using
        /// transform hierarchy.
        /// </summary>
        /// <param name="valueType"></param>
        /// <param name="sourceObj"></param>
        /// <param name="targetObj"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static UnityEngine.Object GetCorrectReferenceValue(Type valueType, UnityEngine.Object sourceObj, UnityEngine.Object targetObj, GameObject source, GameObject target)
        {
            if (typeof(Component).IsAssignableFrom(valueType))
            {
                Component sourceComp = (Component)sourceObj;
                Component targetComp = (Component)targetObj;

                // If the prefab has no reference...
                if (sourceComp == null)
                {
                    // Check if we are a self || child reference...
                    if (targetComp != null)
                    {
                        if (targetComp.transform == target.transform || uPrefabUtility.IsChildOf(target.transform, targetComp.transform))
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    // Check for relative component reference...
                    if (sourceComp.transform == source.transform || uPrefabUtility.IsChildOf(source.transform, sourceComp.transform))
                    {
                        Component newTargetObj = uPrefabUtility.GetInstanceComponentFromAssetReference(sourceComp, source, target);

                        return newTargetObj;
                    }
                    else
                    {
                        return sourceComp;
                    }
                }

                return targetComp;
            }

            string sourceAssetPath = AssetDatabase.GetAssetPath(sourceObj);

            if (typeof(GameObject).IsAssignableFrom(valueType))
            {
                // This is an object reference to something...
                if (sourceAssetPath.Contains(".prefab"))
                {
                    // Same prefab...
                    if (sourceAssetPath == AssetDatabase.GetAssetPath(source))
                    {
                        // is it self...
                        if (sourceObj == source)
                        {
                            return target;
                        }
                        else
                        {
                            string assetChildTransformPath = uPrefabUtility.GetChildTransformPath(source.transform, ((GameObject)sourceObj).transform);
                            Transform instanceChildTransform = target.transform.FindChild(assetChildTransformPath);

                            if (instanceChildTransform != null)
                            {
                                return instanceChildTransform.gameObject;
                            }
                        }
                    }
                    else // Some other prefab or object...
                    {
                        return sourceObj;
                    }
                }
                else
                {
                    return sourceObj;
                }

                return targetObj;
            }
            else
            {
                if (!sourceAssetPath.Contains(".prefab"))
                {
                    return sourceObj;
                }
            }

            return targetObj;
        }

        /// <summary>
        /// Using the transform hierarchy, finds an instance gameObject based on the 
        /// assigned assetReference.
        /// </summary>
        /// <param name="assetReference"></param>
        /// <param name="asset"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static GameObject GetInstanceGameObjectFromAssetReference(GameObject assetReference, GameObject asset, GameObject instance)
        {
            if (assetReference == asset)
            {
                return instance;
            }
            else
            {
                string assetChildTransformPath = uPrefabUtility.GetChildTransformPath(asset.transform, assetReference.transform);
                Transform instanceChildTransform = instance.transform.FindChild(assetChildTransformPath);

                if (instanceChildTransform != null)
                {
                    return instanceChildTransform.gameObject;
                }
            }

            return null;
        }

        /// <summary>
        /// Using the transform hierarchy, finds an instance component based on the assigned assetReference.
        /// </summary>
        /// <param name="assetComponent"></param>
        /// <param name="asset"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Component GetInstanceComponentFromAssetReference(Component assetComponent, GameObject asset, GameObject instance)
        {
            Transform assetComponentTransform = assetComponent.transform;

            // On child...
            if (IsChildOf(asset.transform, assetComponentTransform))
            {
                string assetComponentTransformPath = GetChildTransformPath(asset.transform, assetComponentTransform);
                Transform instanceComponentTransform = instance.transform.FindChild(assetComponentTransformPath);

                if (instanceComponentTransform)
                {
                    return instanceComponentTransform.GetComponent(assetComponent.GetType());
                }
            }

            // On self...
            if (asset.transform == assetComponentTransform)
            {
                return instance.GetComponent(assetComponent.GetType());
            }

            return null;
        }

        public static bool IsDescendentOf(Transform self, Transform target)
        {
            Transform parent = self.parent;

            while (parent != null)
            {
                if (parent == target)
                {
                    return true;
                }
                else
                {
                    parent = parent.parent;
                }
            }

            return false;
        }

        public static bool IsChildOf(Transform parent, Transform child)
        {
            Transform tempParent = child.parent;

            if (tempParent == parent)
            {
                return true;
            }

            while (tempParent != null && tempParent != parent)
            {
                tempParent = tempParent.parent;

                if (tempParent == parent)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetChildTransformPath(Transform parent, Transform child)
        {
            string path = child.name;
            Transform tempParent = child.parent;

            while (tempParent != null && tempParent != parent)
            {
                path = tempParent.name + "/" + path;
                tempParent = tempParent.parent;
            }

            return path;
        }

        public static string GetPath(Transform target)
        {
            string path = target.name;
            Transform tempParent = target.parent;

            while (tempParent != null)
            {
                path = tempParent.name + "/" + path;

                tempParent.parent = target.parent;
            }

            return path;
        }

        /// <summary>
        /// Finds all prefab objects that exist in the project.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GameObject> FindAllPrefabObjects()
        {
            var results = from assetPath in AssetDatabase.GetAllAssetPaths()
                          where assetPath.EndsWith(".prefab", StringComparison.CurrentCulture)
                          select AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            return results;
        }

        /// <summary>
        /// Finds all scene asset paths in the project.
        /// </summary>
        /// <returns>The all scene paths.</returns>
        public static IEnumerable<string> FindAllScenePaths()
        {
            var results = from assetPath in AssetDatabase.GetAllAssetPaths()
                          where assetPath.EndsWith(".unity", StringComparison.CurrentCulture)
                          select assetPath;

            return results;
        }

        public static bool DoObjectsMatch(Type type, object sourceVal, object targetVal, Component sourceComp, Component targetComp)
        {
            if(type == null)
            {
                return false;
            }

            // Check matching null states...
            if((sourceVal == null && targetVal == null))
            {
                return true;
            }

            // Check any null vs. not-null...
            if ((sourceVal != null && sourceVal == null) || (sourceVal == null && targetVal != null))
            {
                return false;
            }

            if (typeof(IList).IsAssignableFrom(type))
            {
                Type elementType = (type.IsArray) ? type.GetElementType() : null;
                elementType = (elementType == null && type.GetGenericArguments().Count() > 0) ? type.GetGenericArguments()[0] : typeof(object);

                IList sourceList = (IList)sourceVal;
                IList targetList = (IList)targetVal;

                // Check if either is null...
                if ((sourceList == null && targetList != null) || (sourceList != null && targetList == null))
                {
                    //Debug.Log(sourceList);
                    return false;
                }

                // Check for length...
                if (sourceList.Count != targetList.Count)
                {
                    //Debug.Log(sourceList.Count);
                    //Debug.Log(targetList.Count);
                    return false;
                }

                for (int i = 0; i < sourceList.Count; i++)
                {
                    object aObj1 = sourceList[i];
                    object aObj2 = targetList[i];

                    //Debug.Log(aObj1);
                    //Debug.Log(aObj2);

                    bool doAValueMatch = DoObjectsMatch(elementType, aObj1, aObj2, sourceComp, targetComp);

                    //Debug.Log(doAValueMatch);

                    if (!doAValueMatch)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                if (typeof(Gradient).IsAssignableFrom(type))
                {
                    Gradient gradient1 = (Gradient)sourceVal;
                    Gradient gradient2 = (Gradient)targetVal;

                    return GradientMatches(gradient1, gradient2);
                }

                if (typeof(AnimationCurve).IsAssignableFrom(type))
                {
                    AnimationCurve curve1 = (AnimationCurve)sourceVal;
                    AnimationCurve curve2 = (AnimationCurve)targetVal;

                    return AnimationCurveMatches(curve1, curve2);
                }

                if (typeof(RectOffset).IsAssignableFrom(type))
                {
                    RectOffset offset1 = (RectOffset)sourceVal;
                    RectOffset offset2 = (RectOffset)targetVal;

                    return offset1.ToString() == offset2.ToString();
                }

                if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                {
                    UnityEngine.Object obj1 = (UnityEngine.Object)sourceVal;
                    UnityEngine.Object obj2 = (UnityEngine.Object)targetVal;

                    if (obj1 == null && obj2 == null)
                    {
                        return true;
                    }

                    // If either object is null and the other isn't...
                    if ((obj1 == null && obj2 != null) || (obj1 != null && obj2 == null))
                    {
                        return false;
                    }

                    // Most likely a relative object?
                    if (EditorUtility.IsPersistent(obj1) && !EditorUtility.IsPersistent(obj2))
                    {
                        var relativeObj = uPrefabUtility.GetCorrectReferenceValue(type, obj1, obj2, sourceComp.gameObject, targetComp.gameObject);

                        if (relativeObj == obj2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    return obj1 == obj2;
                }

                return System.Object.Equals(sourceVal, targetVal);
            }
        }

        public static bool GradientMatches(Gradient a, Gradient b)
        {
            return (ArrayEquals(a.alphaKeys, b.alphaKeys) && ArrayEquals(a.colorKeys, b.colorKeys));
        }

        public static bool AnimationCurveMatches(AnimationCurve a, AnimationCurve b)
        {
            return ArrayEquals(a.keys, b.keys);
        }

        static bool ArrayEquals(Array a1, Array a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (!System.Object.Equals(a1.GetValue(i), a2.GetValue(i))) return false;
            }

            return true;
        }


        /// <summary>
        /// Get alls components whose values differ from the asset.
        /// </summary>
        /// <returns>The modified components.</returns>
        /// <param name="prefab">Prefab.</param>
        public static List<Component> GetModifiedComponents(GameObject instance, GameObject asset)
        {
            List<Component> dirtyComponents = new List<Component>();

            foreach (Component instanceComponent in instance.GetComponents<Component>())
            {
                if (instanceComponent == null)
                {
                    continue;
                }

                if (typeof(Transform).IsAssignableFrom(instanceComponent.GetType())) { continue; }
                if (typeof(uObject).IsAssignableFrom(instanceComponent.GetType())) { continue; }

                Component assetComponent = asset.GetComponent(instanceComponent.GetType());

                if (assetComponent)
                {
                    uPrefabSerializedProperty property = uPrefabSerializedProperty.GetProperty(instanceComponent.GetType());

                    foreach (var field in property.fields)
                    {
                        object val1 = field.GetValue(assetComponent);
                        object val2 = field.GetValue(instanceComponent);

                        if (!DoObjectsMatch(field.FieldType, val1, val2, assetComponent, instanceComponent))
                        {
                            if (!dirtyComponents.Contains(instanceComponent))
                            {
                                dirtyComponents.Add(instanceComponent);
                            }
                            break;
                        }
                    }

                    foreach (var prop in property.properties)
                    {
                        object val1 = prop.GetValue(assetComponent, null);
                        object val2 = prop.GetValue(instanceComponent, null);

                        if (!DoObjectsMatch(prop.PropertyType, val1, val2, assetComponent, instanceComponent))
                        {
                            if (!dirtyComponents.Contains(instanceComponent))
                            {
                                dirtyComponents.Add(instanceComponent);
                            }
                            break;
                        }

                    }

                }
            }

            return dirtyComponents;
        }

        /// <summary>
        /// Returns all components that are missing from this instance.
        /// </summary>
        /// <returns>The missing components.</returns>
        /// <param name="prefab">Prefab.</param>
        public static List<Component> GetMissingComponents(GameObject instance, GameObject asset)
        {
            List<Component> dirtyComponents = new List<Component>();

            foreach (Component assetComponent in asset.GetComponents<Component>())
            {
                if (!assetComponent) { continue; }
                if (typeof(Transform).IsAssignableFrom(assetComponent.GetType())) { continue; }
                if (typeof(uObject).IsAssignableFrom(assetComponent.GetType())) { continue; }

                Component instanceComponent = instance.GetComponent(assetComponent.GetType());

                if (!instanceComponent)
                {
                    dirtyComponents.Add(assetComponent);
                }
            }

            return dirtyComponents;
        }

        /// <summary>
        /// Gets all components that have been added to this isntance.
        /// </summary>
        /// <returns>The added components.</returns>
        public static List<Component> GetAddedComponents(GameObject instance, GameObject asset)
        {
            List<Component> dirtyComponents = new List<Component>();

            foreach (Component instanceComponent in instance.GetComponents<Component>())
            {
                if (!instanceComponent) { continue; }
                if (typeof(Transform).IsAssignableFrom(instanceComponent.GetType())) { continue; }
                if (typeof(uObject).IsAssignableFrom(instanceComponent.GetType())) { continue; }

                Component assetComponent = asset.GetComponent(instanceComponent.GetType());

                if (!assetComponent)
                {
                    dirtyComponents.Add(instanceComponent);
                }
            }

            return dirtyComponents;
        }

    }

}
