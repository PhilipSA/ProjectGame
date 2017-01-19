using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AntiheroStudios.uPrefabs.Editor
{
    public class uPrefabApplyPopupWindow : PopupWindowContent
    {
        public event Action onApplySelf;
        public event Action onApplyHierarchy;
        public event Action<Component> onApplyComponent;

        /// <summary>
        /// The uPrefab or uPrefabChild that is the target of this window.
        /// </summary>
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
            if (!target)
            {
                return;
            }

            EditorStyles.miniLabel.richText = true;
            GUILayout.BeginVertical();
            GUILayout.Label("Apply Tools", EditorStyles.miniBoldLabel);

            if (GUILayout.Button(new GUIContent("Apply Self & Parent(s)", "Updates the prefab asset and parent prefabs"), EditorStyles.miniButton))
            {
                if (onApplySelf != null)
                {
                    onApplySelf();
                }
            }

            if (GUILayout.Button(new GUIContent("Apply Parent(s) Only", "Applies only the parent prefabs, preserving the original asset"), EditorStyles.miniButton))
            {
                if (onApplyHierarchy != null)
                {
                    onApplyHierarchy();
                }
            }

            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            Component[] components = (from comp in target.GetComponents<Component>()
                                      where comp != null && comp.GetType() != typeof(uPrefab) && comp.GetType() != typeof(uPrefabChild) && !typeof(Transform).IsAssignableFrom(comp.GetType())
                                      select comp).ToArray();

            if (components.Length > 0)
            {
                GUILayout.Label("Components", EditorStyles.miniBoldLabel);

                if (!showUnmodifiedComponents && modifiedComponents.Count == 0)
                {
                    GUILayout.Label("No Modified Components", EditorStyles.miniLabel);
                }
                else
                {
                    for (int i = 0; i < components.Length; i++)
                    {
                        if(!showUnmodifiedComponents && !IsModifiedComponentType(components[i].GetType()))
                        {
                            continue;
                        }

                        var buttonContent = EditorGUIUtility.ObjectContent(components[i], components[i].GetType());

                        if (GUILayout.Button(new GUIContent("Apply " + components[i].GetType().Name, buttonContent.image), EditorStyles.miniButton, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                        {
                            if (onApplyComponent != null)
                            {
                                onApplyComponent(components[i]);
                            }
                        }
                    }
                }
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
            foreach (Component component in modifiedComponents)
            {
                if (component.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}