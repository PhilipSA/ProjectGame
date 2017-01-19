using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;

namespace AntiheroStudios.uPrefabs.Editor
{
    [InitializeOnLoad]
    public static class uPrefabEditorCallbacks
    {
        static bool stylesSetup = false;
        static GUIStyle kTooltipStyle;
        static GUIStyle kPrefabLabelStyle;
        static List<uObject> kUpdateObjects;
        static int kIndex = 0;

        static uPrefabEditorCallbacks()
        {
            EditorApplication.hierarchyWindowItemOnGUI += EditorApplication_HierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowChanged += EditorApplication_HierarchyWindowChanged;
            EditorApplication.update += EditorApplication_Update;
        }

        static void CalculateComponentInfo(GameObject gameObject)
        {
            uPrefab prefab = gameObject.GetComponent<uPrefab>();
            uPrefabChild prefabChild = gameObject.GetComponent<uPrefabChild>();

            if (prefab && prefab.asset)
            {
                OnCalculateComponentInfo(prefab, prefab.asset);
            }

            if (prefabChild && prefabChild.asset)
            {
                OnCalculateComponentInfo(prefabChild, prefabChild.asset);
            }
        }

        static void EditorApplication_HierarchyWindowChanged()
        {
            kIndex = 0;
            kUpdateObjects = new List<uObject>();
            GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].GetComponent<uObject>())
                {
                    kUpdateObjects.Add(objects[i].GetComponent<uObject>());
                }

                kUpdateObjects.AddRange(objects[i].GetComponentsInChildren<uObject>(true));
            }
        }

        static void EditorApplication_Update()
        {
            if (!Application.isPlaying)
            {
                if (kUpdateObjects != null)
                {
                    if (kIndex > kUpdateObjects.Count - 1)
                    {
                        kIndex = 0;
                    }
                    else
                    {
                        if (kUpdateObjects[kIndex] != null)
                        {
                            CalculateComponentInfo(kUpdateObjects[kIndex].gameObject);
                        }

                        kIndex++;
                    }
                }
            }
        }

        static void EditorApplication_HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject gameObject = (UnityEngine.GameObject)EditorUtility.InstanceIDToObject(instanceID);

            if (gameObject)
            {
                uObject prefab = gameObject.GetComponent<uObject>();

                if (prefab && prefab.asset)
                {
                    OnDrawHierarchyGUI(instanceID, selectionRect, prefab, prefab.asset);
                }
            }
        }

        public static void OnCalculateComponentInfo(uObject instance, uObject asset)
        {
            if (!Application.isPlaying)
            {
                instance.childrenChanged = instance.transform.childCount != asset.transform.childCount;
                instance.modifiedComponents = uPrefabUtility.GetModifiedComponents(instance.gameObject, asset.gameObject).ToArray();
                instance.addedComponents = uPrefabUtility.GetAddedComponents(instance.gameObject, asset.gameObject).ToArray();
                instance.removedComponents = uPrefabUtility.GetMissingComponents(instance.gameObject, asset.gameObject).ToArray();
            }
        }

        static void OnDrawHierarchyGUI(int instanceId, Rect selectionRect, uObject instance, uObject asset)
        {
            if (!stylesSetup)
            {
                kTooltipStyle = GUI.skin.FindStyle("Tooltip");
                kTooltipStyle.richText = true;
                kTooltipStyle.alignment = TextAnchor.MiddleCenter;

                kPrefabLabelStyle = new GUIStyle(EditorStyles.miniLabel);
                kPrefabLabelStyle.alignment = TextAnchor.MiddleCenter;
                kPrefabLabelStyle.normal.textColor = new Color(kPrefabLabelStyle.normal.textColor.r, kPrefabLabelStyle.normal.textColor.g, kPrefabLabelStyle.normal.textColor.b, .25f);
                kPrefabLabelStyle.richText = true;
            }

            kPrefabLabelStyle.fontStyle = FontStyle.Normal;

            string assetName = asset.name;
            string childrenChangedText = string.Empty;
            string dirtyComponentsText = string.Empty;
            string addedComponentsText = string.Empty;
            string missingcomponentsText = string.Empty;
            string tooltipMessage = string.Empty;

            if (Event.current.type == EventType.Repaint)
            {
                OnCalculateComponentInfo(instance, asset);

                if (instance.modifiedComponents != null && (instance.modifiedComponents.Length > 0))
                {
                    kPrefabLabelStyle.normal.textColor = uPrefabMenuItems.modifiedColor;
                    kPrefabLabelStyle.fontStyle = FontStyle.Bold;

                    var names = (from c in instance.modifiedComponents
                                 select c.GetType().Name).ToArray();

                    dirtyComponentsText = "\n<color=#" + ColorToHex(uPrefabMenuItems.modifiedColor) + ">Modified Components: " + string.Join(", ", names) + "</color>";
                }

                if (instance.childrenChanged)
                {
                    kPrefabLabelStyle.normal.textColor = uPrefabMenuItems.modifiedColor;
                    kPrefabLabelStyle.fontStyle = FontStyle.Bold;

                    childrenChangedText = "\n\n<i>Unapplied Transform Children Changes</i>";
                }

                if (instance.addedComponents != null && instance.addedComponents.Length > 0)
                {
                    kPrefabLabelStyle.normal.textColor = uPrefabMenuItems.addedColor;

                    var names = (from c in instance.addedComponents
                                 select c.GetType().Name).ToArray();

                    addedComponentsText = "\n<color=#" + ColorToHex(uPrefabMenuItems.addedColor) + ">Added Components: " + string.Join(", ", names) + "</color>";
                }

                if (instance.removedComponents != null && instance.removedComponents.Length > 0)
                {
                    kPrefabLabelStyle.normal.textColor = uPrefabMenuItems.missingColor;

                    var names = (from c in instance.removedComponents
                                 select c.GetType().Name).ToArray();

                    missingcomponentsText = "\n<color=#" + ColorToHex(uPrefabMenuItems.missingColor) + ">Missing Components: " + string.Join(", ", names) + "</color>";
                }

                string assetMessage = "This GameObject is an instance of the <b>" + assetName + "</b> prefab.";

                if (asset.GetComponent<uPrefabChild>())
                {
                    assetName = asset.GetComponent<uPrefabChild>().uPrefabParent.name;
                    assetMessage = "This GameObject is a child of the <b>" + assetName + "</b> prefab.";
                }

                tooltipMessage = assetMessage + dirtyComponentsText + addedComponentsText + missingcomponentsText + childrenChangedText;
            }

            GUIContent prefabLabelContent = new GUIContent("(" + assetName + ")", tooltipMessage);

            Vector2 hierarchySize = EditorStyles.foldout.CalcSize(new GUIContent(instance.name));
            Vector2 size = kPrefabLabelStyle.CalcSize(prefabLabelContent);

            Rect prefabLabelRect = new Rect(selectionRect.x + hierarchySize.x, selectionRect.y, size.x + 5, selectionRect.height);

            GUI.Label(prefabLabelRect, prefabLabelContent, kPrefabLabelStyle);
        }

        static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }
    }
}