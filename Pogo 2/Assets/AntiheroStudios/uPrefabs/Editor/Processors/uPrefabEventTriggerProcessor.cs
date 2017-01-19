using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(EventTrigger))]
    public class uPrefabEventTriggerProcessor : uPrefabComponentProcessor
    {
        #region Persistent Calls Reflections
        private FieldInfo unityEventBase_mPersistentCalls
        {
            get
            {
                return typeof(UnityEventBase).GetField("m_PersistentCalls", BindingFlags.Instance | BindingFlags.NonPublic);
            }
        }

        private FieldInfo mPersistentCalls_mCalls
        {
            get
            {
                return unityEventBase_mPersistentCalls.FieldType.GetField("m_Calls", BindingFlags.Instance | BindingFlags.NonPublic);
            }
        }

        private Type mPersistentCallType
        {
            get
            {
                return mPersistentCalls_mCalls.FieldType.GetGenericArguments()[0];
            }
        }

        private FieldInfo persistentCall_mTarget
        {
            get
            {
                return mPersistentCallType.GetField("m_Target", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        private FieldInfo persistentCall_mMethodName
        {
            get
            {
                return mPersistentCallType.GetField("m_MethodName", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        private FieldInfo persistentCall_mMode
        {
            get
            {
                return mPersistentCallType.GetField("m_Mode", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        private FieldInfo persistentCall_mCallState
        {
            get
            {
                return mPersistentCallType.GetField("m_CallState", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        private FieldInfo persistentCall_mArguments
        {
            get
            {
                return mPersistentCallType.GetField("m_Arguments", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        private object GetPersistentCallGroup(EventTrigger.TriggerEvent triggerEvent)
        {
            return unityEventBase_mPersistentCalls.GetValue(triggerEvent);
        }

        private IList GetCalls(object persistentCallGroup)
        {
            return (IList)mPersistentCalls_mCalls.GetValue(persistentCallGroup);
        }

        private object CreatePersistentCallGroup()
        {
            return Activator.CreateInstance(unityEventBase_mPersistentCalls.FieldType);
        }

        private object CreatePersistentCall()
        {
            return Activator.CreateInstance(mPersistentCallType);
        }
        #endregion

        #region Arguments Cache Reflection
        private object GetArgumentCache(object persistentCall)
        {
            return (object)persistentCall_mArguments.GetValue(persistentCall);
        }

        private object CreateArgumentCache()
        {
            return Activator.CreateInstance(persistentCall_mArguments.FieldType);
        }

        private PropertyInfo argumentCache_mObjectArgument
        {
            get
            {
                return persistentCall_mArguments.FieldType.GetProperty("unityObjectArgument", BindingFlags.Instance | BindingFlags.Public);
            }
        }

        private PropertyInfo argumentCache_mIntArgument
        {
            get
            {
                return persistentCall_mArguments.FieldType.GetProperty("intArgument", BindingFlags.Instance | BindingFlags.Public);
            }
        }

        private PropertyInfo argumentCache_mFloatArgument
        {
            get
            {
                return persistentCall_mArguments.FieldType.GetProperty("floatArgument", BindingFlags.Instance | BindingFlags.Public);
            }
        }

        private PropertyInfo argumentCache_mStringArgument
        {
            get
            {
                return persistentCall_mArguments.FieldType.GetProperty("stringArgument", BindingFlags.Instance | BindingFlags.Public);
            }
        }

        private PropertyInfo argumentCache_mBoolArgument
        {
            get
            {
                return persistentCall_mArguments.FieldType.GetProperty("boolArgument", BindingFlags.Instance | BindingFlags.Public);
            }
        }
        #endregion

        /// <summary>
        /// Since EventTrigger has some deep nested & internal classes, we must use reflection
        /// to appropriately revert.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceComponent"></param>
        /// <param name="target"></param>
        /// <param name="targetComponent"></param>
        public override void OnRevertComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            this.OnCopyObject(componentType, asset, instance, assetComponent, instanceComponent);
        }

        public override void OnApplyComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            this.OnCopyObject(componentType, instance, asset, instanceComponent, assetComponent);

            SerializedObject instanceSO = new SerializedObject(instanceComponent);
            SerializedProperty instanceProp = instanceSO.GetIterator();

            while (instanceProp.Next(true))
            {
                switch(instanceProp.name)
                {
                    case "m_Delegates":
                    case "m_PersistentCalls":
                        instanceProp.prefabOverride = false;
                        break;
                }
            }

            instanceSO.ApplyModifiedProperties();
        }  

        protected override void OnCopyObject(Type objectType, GameObject fromGameObject, GameObject toGameObject, object fromObject, object toObject)
        {
            EventTrigger sourceTrigger = (EventTrigger)fromObject;
            EventTrigger targetTrigger = (EventTrigger)toObject;

            targetTrigger.triggers.Clear();

            for (int i = 0; i < sourceTrigger.triggers.Count; i++)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.callback = new EventTrigger.TriggerEvent();
                entry.eventID = sourceTrigger.triggers[i].eventID;

                object sourcePersistentCallGroup = GetPersistentCallGroup(sourceTrigger.triggers[i].callback);
                object targetPersistentCallGroup = GetPersistentCallGroup(entry.callback);
                IList sourcePersistentCalls = GetCalls(sourcePersistentCallGroup);
                IList targetPersistentCalls = GetCalls(targetPersistentCallGroup);

                for (int j = 0; j < sourcePersistentCalls.Count; j++)
                {
                    object targetCall = CreatePersistentCall();

                    #region Setup Target
                    UnityEngine.Object sourceTarget = (UnityEngine.Object)persistentCall_mTarget.GetValue(sourcePersistentCalls[j]);
                    UnityEngine.Object targetTarget = sourceTarget;

                    if(typeof(UnityEngine.Object).IsAssignableFrom(sourceTarget.GetType()))
                    {
                        targetTarget = uPrefabUtility.GetCorrectReferenceValue(sourceTarget.GetType(), sourceTarget, targetTarget, fromGameObject, toGameObject);
                    }
                    #endregion

                    #region Setup Arguments
                    object sourceArgumentsCache = GetArgumentCache(sourcePersistentCalls[j]);
                    object targetArgumentsCache = CreateArgumentCache();

                    argumentCache_mBoolArgument.SetValue(targetArgumentsCache, argumentCache_mBoolArgument.GetValue(sourceArgumentsCache, null), null);
                    argumentCache_mIntArgument.SetValue(targetArgumentsCache, argumentCache_mIntArgument.GetValue(sourceArgumentsCache, null), null);
                    argumentCache_mFloatArgument.SetValue(targetArgumentsCache, argumentCache_mFloatArgument.GetValue(sourceArgumentsCache, null), null);
                    argumentCache_mStringArgument.SetValue(targetArgumentsCache, argumentCache_mStringArgument.GetValue(sourceArgumentsCache, null), null);
                    argumentCache_mObjectArgument.SetValue(targetArgumentsCache, argumentCache_mObjectArgument.GetValue(sourceArgumentsCache, null), null);

                    object arg = argumentCache_mObjectArgument.GetValue(targetArgumentsCache, null);

                    if (arg != null)
                    {
                        if (typeof(GameObject).IsAssignableFrom(arg.GetType()))
                        {
                            arg = uPrefabUtility.GetCorrectReferenceValue(arg.GetType(), (UnityEngine.Object)arg, (UnityEngine.Object)arg, fromGameObject, toGameObject);
                            argumentCache_mObjectArgument.SetValue(targetArgumentsCache, arg, null);
                        }

                        if (typeof(Component).IsAssignableFrom(arg.GetType()))
                        {
                            arg = uPrefabUtility.GetCorrectReferenceValue(arg.GetType(), (UnityEngine.Object)arg, (UnityEngine.Object)arg, fromGameObject, toGameObject);
                            argumentCache_mObjectArgument.SetValue(targetArgumentsCache, arg, null);
                        }
                    }
                    #endregion

                    persistentCall_mTarget.SetValue(targetCall, targetTarget);
                    persistentCall_mArguments.SetValue(targetCall, targetArgumentsCache);
                    persistentCall_mMode.SetValue(targetCall, persistentCall_mMode.GetValue(sourcePersistentCalls[j]));
                    persistentCall_mMethodName.SetValue(targetCall, persistentCall_mMethodName.GetValue(sourcePersistentCalls[j]));
                    persistentCall_mCallState.SetValue(targetCall, persistentCall_mCallState.GetValue(sourcePersistentCalls[j]));

                    targetPersistentCalls.Add(targetCall);
                }

                targetTrigger.triggers.Add(entry);
            }
        }
    }
}