using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    /// <summary>
    /// A class that specifies how a component is copied to a target component. Can be
    /// used to customize the process for saving to an asset prefab or updating a target instance prefab.
    /// </summary>
    public class uPrefabComponentProcessor
    {
        public static bool kUseProcessors = true;

        private static Dictionary<Type, List<uPrefabComponentProcessor>> m_componentProcessors = new Dictionary<Type, List<uPrefabComponentProcessor>>();

        /// <summary>
        /// Specified by the attribute. Determines what order this processor will execute if
        /// there are multiple processors for a component type.
        /// </summary>
        public int priority = 0;

        public virtual List<string> SkipSerializedPropertyNames
        {
            get
            {
                return new List<string>(new string[] { "hideFlags", "name" });
            }
        }

        public virtual void OnRevertComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            this.OnCopyObject(componentType, asset, instance, assetComponent, instanceComponent);
        }

        public virtual void OnApplyComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            /*
            if (PrefabUtility.GetPrefabParent(instance) == asset)
            {
                assetComponent = instanceComponent;
                UnityEngine.Object.DestroyImmediate(instanceComponent);
            }
            else
            {
                EditorUtility.CopySerialized(instanceComponent, assetComponent);
            }*/

            this.OnCopyObject(componentType, instance, asset, instanceComponent, assetComponent);
        }

        protected virtual void OnCopyObject(Type objectType, GameObject fromGameObject, GameObject toGameObject, object fromObject, object toObject)
        {
            var serializedProperty = uPrefabSerializedProperty.GetProperty(objectType);

            foreach (var field in serializedProperty.fields)
            {
                object sourceValue = (fromObject != null) ? field.GetValue(fromObject) : null;
                object targetValue = (toObject != null) ? field.GetValue(toObject) : null;

                if (typeof(IList).IsAssignableFrom(field.FieldType))
                {
                    object assignedValue = this.OnCopyObjectArrayProperty(field.FieldType, fromGameObject, toGameObject, sourceValue, targetValue);

                    field.SetValue(toObject, assignedValue);
                }
                else
                {
                    object assignedValue = this.OnCopyObjectSingleProperty(field.FieldType, fromGameObject, toGameObject, sourceValue, targetValue);

                    field.SetValue(toObject, assignedValue);
                }
            }

            foreach (var property in serializedProperty.properties)
            {
                object sourceValue = (fromObject != null) ? property.GetValue(fromObject, null) : null;
                object targetValue = (toObject != null) ? property.GetValue(toObject, null) : null;

                if (typeof(IList).IsAssignableFrom(property.PropertyType))
                {
                    object assignedValue = this.OnCopyObjectArrayProperty(property.PropertyType, fromGameObject, toGameObject, sourceValue, targetValue);

                    property.SetValue(toObject, assignedValue, null);
                }
                else
                {
                    object assignedValue = this.OnCopyObjectSingleProperty(property.PropertyType, fromGameObject, toGameObject, sourceValue, targetValue);

                    property.SetValue(toObject, assignedValue, null);
                }
            }
        }

        protected virtual object OnCopyObjectSingleProperty(Type propertyType, GameObject fromGameObject, GameObject toGameObject, object fromObject, object toObject)
        {
            if (!typeof(UnityEngine.Object).IsAssignableFrom(propertyType))
            {
                bool hasSerializableAttribute = propertyType.GetCustomAttributes(typeof(System.SerializableAttribute), true).Length > 0;

                if (hasSerializableAttribute && propertyType.IsClass)
                {
                    this.OnCopyObject(propertyType, fromGameObject, toGameObject, fromObject, toObject);

                    return toObject;
                }
                else
                {
                    return fromObject;
                }
            }
            else
            {
                var sourceObj = (UnityEngine.Object)fromObject;
                var targetObj = (UnityEngine.Object)toObject;

                var correctObjReference = uPrefabUtility.GetCorrectReferenceValue(propertyType, sourceObj, targetObj, fromGameObject, toGameObject);

                return correctObjReference;
            }
        }

        protected virtual object OnCopyObjectArrayProperty(Type propertyType, GameObject fromGameObject, GameObject toGameObject, object fromObject, object toObject)
        {
            Type elementType = GetElementType(propertyType);

            if (!typeof(UnityEngine.Object).IsAssignableFrom(elementType))
            {
                //bool hasSerializableAttribute = elementType.GetCustomAttributes(typeof(System.SerializableAttribute), true).Length > 0;

                IList sourceObjArray = (IList)fromObject;
                IList targetObjArray = (IList)toObject;

                // Size too big?
                if (targetObjArray.Count > sourceObjArray.Count)
                {
                    if (propertyType.IsArray)
                    {
                        Array newTargetObjArray = Array.CreateInstance(elementType, sourceObjArray.Count);
                        Array.ConstrainedCopy((Array)targetObjArray, 0, newTargetObjArray, 0, sourceObjArray.Count);

                        targetObjArray = newTargetObjArray;
                    }
                    else
                    {
                        ArrayList newTargetObjArray = ArrayList.Adapter(targetObjArray);

                        while (newTargetObjArray.Count > sourceObjArray.Count)
                        {
                            newTargetObjArray.RemoveAt(newTargetObjArray.Count - 1);
                        }
                    }
                }

                // Size too small?
                if(targetObjArray.Count < sourceObjArray.Count)
                {
                    if (propertyType.IsArray)
                    {
                        Array newTargetObjArray = Array.CreateInstance(elementType, sourceObjArray.Count);
                        Array.ConstrainedCopy((Array)targetObjArray, 0, newTargetObjArray, 0, targetObjArray.Count);

                        targetObjArray = newTargetObjArray;
                    }
                    else
                    {
                        ArrayList newTargetObjArray = ArrayList.Adapter(targetObjArray);

                        while (newTargetObjArray.Count < sourceObjArray.Count)
                        {
                            if (elementType.IsValueType)
                            {
                                newTargetObjArray.Add(Activator.CreateInstance(elementType));
                            }
                            else
                            {
                                newTargetObjArray.Add(null);
                            }
                        }
                    }
                }

                for (int i = 0; i < sourceObjArray.Count; i++)
                {
                    targetObjArray[i] = sourceObjArray[i];
                }

                return targetObjArray;
            }
            else
            {
                IList sourceObjArray = (IList)fromObject;
                IList targetObjArray = (IList)toObject;

                // Size too big?
                if (targetObjArray.Count > sourceObjArray.Count)
                {
                    if (propertyType.IsArray)
                    {
                        Array newTargetObjArray = Array.CreateInstance(elementType, sourceObjArray.Count);
                        Array.ConstrainedCopy((Array)targetObjArray, 0, newTargetObjArray, 0, sourceObjArray.Count);

                        targetObjArray = newTargetObjArray;
                    }
                    else
                    {
                        ArrayList newTargetObjArray = ArrayList.Adapter(targetObjArray);

                        while (newTargetObjArray.Count > sourceObjArray.Count)
                        {
                            newTargetObjArray.RemoveAt(newTargetObjArray.Count - 1);
                        }
                    }
                }

                // Size too small?
                if (targetObjArray.Count < sourceObjArray.Count)
                {
                    if (propertyType.IsArray)
                    {
                        Array newTargetObjArray = Array.CreateInstance(elementType, sourceObjArray.Count);
                        Array.ConstrainedCopy((Array)targetObjArray, 0, newTargetObjArray, 0, targetObjArray.Count);

                        targetObjArray = newTargetObjArray;
                    }
                    else
                    {
                        ArrayList newTargetObjArray = ArrayList.Adapter(targetObjArray);

                        while (newTargetObjArray.Count < sourceObjArray.Count)
                        {
                            newTargetObjArray.Add(null);
                        }
                    }
                }

                for (int i = 0; i < sourceObjArray.Count; i++)
                {
                    UnityEngine.Object sourceObj = (UnityEngine.Object)sourceObjArray[i];
                    UnityEngine.Object targetObj = (UnityEngine.Object)targetObjArray[i];

                    var correctObjReference = uPrefabUtility.GetCorrectReferenceValue(elementType, sourceObj, targetObj, fromGameObject, toGameObject);
                    targetObjArray[i] = correctObjReference;
                }

                return targetObjArray;
            }
        }

        private static Type GetElementType(Type propertyType)
        {
            if (propertyType.IsArray)
            {
                return propertyType.GetElementType();
            }
            else
            {
                if (propertyType.GetGenericArguments().Count() > 0)
                {
                    return propertyType.GetGenericArguments()[0];
                }
                else
                {
                    return typeof(object);
                }
            }
        }

        /// <summary>
        /// Finds a ComponentProcessor for the specified Type.
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public static List<uPrefabComponentProcessor> FindAll(Type componentType)
        {
            if (kUseProcessors)
            {
                if (m_componentProcessors.ContainsKey(componentType))
                {
                    return m_componentProcessors[componentType];
                }
                else
                {
                    List<uPrefabComponentProcessor> processors = new List<uPrefabComponentProcessor>();

                    foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (var t in a.GetTypes())
                        {
                            var attrs = (uPrefabComponentHandlerAttribute[])t.GetCustomAttributes(typeof(uPrefabComponentHandlerAttribute), true);

                            if (attrs.Length > 0)
                            {
                                if (attrs[0].componentType.IsAssignableFrom(componentType) || (attrs[0].inherit && componentType.IsSubclassOf(attrs[0].componentType)))
                                {
                                    uPrefabComponentProcessor processor = (uPrefabComponentProcessor)Activator.CreateInstance(t);
                                    processor.priority = attrs[0].priority;

                                    processors.Add(processor);
                                }
                            }
                        }
                    }

                    // Add the default processor if none exist for the type...
                    if (processors.Count == 0)
                    {
                        processors.Add(new uPrefabComponentProcessor());
                    }

                    m_componentProcessors.Add(componentType, processors.OrderBy(processor => processor.priority).ToList());

                    return m_componentProcessors[componentType];
                }
            }
            else
            {
                List<uPrefabComponentProcessor> processors = new List<uPrefabComponentProcessor>();
                processors.Add(new uPrefabComponentProcessor());

                return processors;
            }
        }
    }


    /// <summary>
    /// An attribute to specify a target class is a
    /// ComponentProcessor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class uPrefabComponentHandlerAttribute : System.Attribute
    {
        /// <summary>
        /// The type of component this processor will target.
        /// </summary>
        public Type componentType;

        /// <summary>
        /// When processing the component copying, each one will be called in
        /// order by their priority. This allows you to chain processors.
        /// </summary>
        public int priority;

        public bool inherit;

        public uPrefabComponentHandlerAttribute(Type componentType, bool inherit = false, int priority = 0)
        {
            this.componentType = componentType;
            this.inherit = inherit;
            this.priority = priority;
        }
    }
}