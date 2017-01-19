using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AntiheroStudios.uPrefabs.Editor 
{
    public class uPrefabSerializedProperty
    {
        static Dictionary<Type, uPrefabSerializedProperty> kCachedProperties = new Dictionary<Type, uPrefabSerializedProperty>();

        public List<FieldInfo> fields = new List<FieldInfo>();
        public List<PropertyInfo> properties = new List<PropertyInfo>();

        public uPrefabSerializedProperty(Type type)
        {
            var processors = uPrefabComponentProcessor.FindAll(type);
            string namespaceStr = (type.Namespace != null) ? type.Namespace : string.Empty;

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                bool skipProperty = false;

                if (field.IsPrivate && field.GetCustomAttributes(typeof(SerializeField), false).Length == 0)
                {
                    skipProperty = true;
                }

                foreach (var processor in processors)
                {
                    if (processor.SkipSerializedPropertyNames.Contains(field.Name))
                    {
                        skipProperty = true;
                        break;
                    }
                }

                if (!skipProperty)
                {
                    fields.Add(field);
                }
            }

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                if (!property.CanRead || !property.CanWrite) { continue; }

                if (!namespaceStr.StartsWith("UnityEngine", StringComparison.Ordinal) && property.GetCustomAttributes(typeof(SerializeField), false).Length == 0) { continue; }
         
                bool skipProperty = false;

                foreach (var processor in processors)
                {
                    if (processor.SkipSerializedPropertyNames.Contains(property.Name))
                    {
                        skipProperty = true;
                        break;
                    }
                }

                if (!skipProperty)
                {
                    properties.Add(property);
                }
            }
        }

        public static uPrefabSerializedProperty GetProperty(Type type)
        {
            if (!kCachedProperties.ContainsKey(type))
            {
                kCachedProperties.Add(type, new uPrefabSerializedProperty(type));
            }

            return kCachedProperties[type];
        }
    }
}