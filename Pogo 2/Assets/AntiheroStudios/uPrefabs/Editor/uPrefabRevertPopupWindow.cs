using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AntiheroStudios.uPrefabs.Editor
{
    public class uPrefabRevertPopupWindow : PopupWindowContent
    {
        public event Action onRevertObject;

        #region Transform Events
        public event Action onRevertPosition;
        public event Action onRevertRotation;
        public event Action onRevertScale;
        #endregion

        #region RectTransform Events
        public event Action onRevertAnchoredPosition;
        public event Action onRevertOffset;
        public event Action onRevertPivot;
        public event Action onRevertAnchors;
        #endregion

        public event Action<Component> onRevertComponent;

        public bool showTransformTools = true;

        public GameObject target;
        public List<Component> modifiedComponents = new List<Component>();
        public bool showUnmodifiedComponents = false;

        private Vector2 m_size = new Vector2(200, 75);

        public override Vector2 GetWindowSize()
        {
            return m_size;
        }

        public override void OnGUI(Rect rect)
        {
            EditorStyles.miniLabel.richText = true;

            GUILayout.BeginVertical();
            GUILayout.Label("Revert Tools", EditorStyles.miniBoldLabel);

            if (GUILayout.Button("Revert Object", EditorStyles.miniButton))
            {
                if (onRevertObject != null)
                {
                    onRevertObject();
                }
            }

            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            if (showTransformTools)
            {
                if (target.GetComponent<RectTransform>())
                {
                    this.OnDrawTransformTools();
                    this.OnDrawRectTransformTools();
                }
                else
                {
                    this.OnDrawTransformTools();
                }
            }

            var targetObj = target.GetComponent<uObject>();

            Component[] components = (from comp in target.GetComponents<Component>()
                                      where comp != null && comp.GetType() != typeof(uPrefab) && comp.GetType() != typeof(uPrefabChild) && !typeof(Transform).IsAssignableFrom(comp.GetType())
                                     select comp).ToArray();

            if (components.Length > 0)
            {
                GUILayout.Label("Components", EditorStyles.miniBoldLabel);

                if (!showUnmodifiedComponents && modifiedComponents.Count == 0)
                {
                    GUILayout.Label("<i>No Modified Components</i>", EditorStyles.miniLabel);
                }
                else
                {
                    for (int i = 0; i < components.Length; i++)
                    {
                        if (components[i].GetType() == typeof(uPrefab) || components[i].GetType() == typeof(uPrefabChild) || typeof(Transform).IsAssignableFrom(components[i].GetType()))
                        {
                            continue;
                        }

                        if (!showUnmodifiedComponents)
                        {
                            if (targetObj)
                            {
                                if (modifiedComponents != null && !IsModifiedComponentType(components[i].GetType()))
                                {
                                    continue;
                                }
                            }
                        }

                        var buttonContent = EditorGUIUtility.ObjectContent(components[i], components[i].GetType());

                        if (GUILayout.Button(new GUIContent("Revert " + components[i].GetType().Name, buttonContent.image), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                        {
                            if (onRevertComponent != null)
                            {
                                onRevertComponent(components[i]);
                            }
                        }
                    }
                }

                //GUILayout.Space(EditorGUIUtility.singleLineHeight);
                //uPrefabComponentProcessor.kUseProcessors = EditorGUILayout.Toggle("Use Processors", uPrefabComponentProcessor.kUseProcessors);
            }

            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            GUILayout.EndVertical();

            Rect size = GUILayoutUtility.GetLastRect();

            if (Event.current.type == EventType.Repaint)
            {
                m_size.y = size.height;
            }

            this.editorWindow.Repaint();
        }

        private bool IsModifiedComponentType(Type type)
        {
            foreach(Component component in modifiedComponents)
            {
                if(component.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawTransformTools()
        {
            GUILayout.Label("Transform", EditorStyles.miniBoldLabel);

            Texture positionIcon = EditorGUIUtility.IconContent("MoveTool").image;
            Texture rotationIcon = EditorGUIUtility.IconContent("RotateTool").image;
            Texture scaleIcon = EditorGUIUtility.IconContent("ScaleTool").image;

            if (UnityEditorInternal.InternalEditorUtility.HasPro())
            {
                positionIcon = EditorGUIUtility.IconContent("MoveTool On").image;
                rotationIcon = EditorGUIUtility.IconContent("RotateTool On").image;
                scaleIcon = EditorGUIUtility.IconContent("ScaleTool On").image;
            }

            if (GUILayout.Button(new GUIContent("Revert Position", positionIcon), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertPosition != null)
                {
                    onRevertPosition();
                }
            }


            if (GUILayout.Button(new GUIContent("Revert Rotation", rotationIcon), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertRotation != null)
                {
                    onRevertRotation();
                }
            }

            if (GUILayout.Button(new GUIContent("Revert Scale", scaleIcon), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertScale != null)
                {
                    onRevertScale();
                }
            }
        }

        private void OnDrawRectTransformTools()
        {
            GUILayout.Label("Rect Transform", EditorStyles.miniBoldLabel);

            if (GUILayout.Button(new GUIContent("Revert Anchored Position 3D"), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertAnchoredPosition != null)
                {
                    onRevertAnchoredPosition();
                }
            }

            if (GUILayout.Button(new GUIContent("Revert Offset"), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertOffset != null)
                {
                    onRevertOffset();
                }
            }

            if (GUILayout.Button(new GUIContent("Revert Pivot"), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertPivot != null)
                {
                    onRevertPivot();
                }
            }

            if (GUILayout.Button(new GUIContent("Revert Anchors"), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                if (onRevertAnchors != null)
                {
                    onRevertAnchors();
                }
            }
        }
    }
}