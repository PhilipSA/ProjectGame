using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AntiheroStudios.uPrefabs.Editor
{
    [CustomEditor(typeof(GameObject), true)]
    [CanEditMultipleObjects]
    public class uPrefabGameObjectCustomInspector : uPrefabBaseGameObjectInspector
    {
        private static Rect activatorRect;

        protected override void OnDrawPrefabButtons()
        {
            uPrefab prefab = Selection.gameObjects[0].GetComponent<uPrefab>();

            if (prefab)
            {
                OnDrawPrefab(prefab);
            }

            uPrefabChild prefabChild = Selection.gameObjects[0].GetComponent<uPrefabChild>();

            if (prefabChild)
            {
                this.OnDrawPrefabChild(prefabChild);
            }
        }

        private void OnDrawPrefab(uPrefab prefab)
        {
            if (prefab.asset)
            {
                GUILayout.BeginHorizontal();

                if (!prefab.isAsset)
                {
                    if (GUILayout.Button(new GUIContent("Select"), EditorStyles.miniButtonLeft))
                    {
                        Selection.activeGameObject = prefab.asset.gameObject;
                    }

                    #region Revert Button
                    bool didClickRevert = GUILayout.Button(new GUIContent("Revert"), EditorStyles.miniButtonMid);

                    if (Event.current.type == EventType.Repaint)
                    {
                        activatorRect = GUILayoutUtility.GetLastRect();
                    }

                    if (didClickRevert)
                    {
                        uPrefabRevertPopupWindow revertPopupWindow = new uPrefabRevertPopupWindow();
                        revertPopupWindow.target = prefab.asset.gameObject;
                        revertPopupWindow.modifiedComponents = uPrefabUtility.GetModifiedComponents(prefab.gameObject, prefab.asset.gameObject);
                        revertPopupWindow.showUnmodifiedComponents = uPrefabMenuItems.showUnmodifiedComponents;

                        revertPopupWindow.onRevertObject += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "Selected instance(s) will be replaced with the prefab asset.", "Revert", "Cancel"))
                            {
                                for (int i = Selection.gameObjects.Length - 1; i >= 0; i--)
                                {
                                    var selectedPrefab = Selection.gameObjects[i].GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(selectedPrefab.gameObject.scene);

                                        uPrefabRevertUtilities.RevertAllOnObject(selectedPrefab);

                                        if (selectedPrefab.uPrefabParent != null)
                                        {
                                            uPrefabApplyUtilities.ApplyUp(selectedPrefab as uPrefab);
                                        }
                                        else
                                        {
                                            uPrefabApplyUtilities.ApplySelf(selectedPrefab);    
                                        }
                                    }
                                }

                                GUIUtility.ExitGUI();
                                EditorApplication.RepaintHierarchyWindow();

                            }
                        };

                        revertPopupWindow.onRevertPosition += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance position!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.transform.localPosition = selectedPrefab.asset.transform.position;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertRotation += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance rotation!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.transform.rotation = selectedPrefab.asset.transform.rotation;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertScale += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance scale!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.transform.localScale = selectedPrefab.asset.transform.localScale;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertAnchoredPosition += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance anchor position!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.GetComponent<RectTransform>().anchoredPosition3D = selectedPrefab.asset.GetComponent<RectTransform>().anchoredPosition3D;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertOffset += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance offset!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.GetComponent<RectTransform>().offsetMin = selectedPrefab.asset.GetComponent<RectTransform>().offsetMin;
                                        selectedPrefab.GetComponent<RectTransform>().offsetMax = selectedPrefab.asset.GetComponent<RectTransform>().offsetMax;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };


                        revertPopupWindow.onRevertPivot += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance pivot!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.GetComponent<RectTransform>().pivot = selectedPrefab.asset.GetComponent<RectTransform>().pivot;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertAnchors += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance anchors!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        selectedPrefab.GetComponent<RectTransform>().anchorMin = selectedPrefab.asset.GetComponent<RectTransform>().anchorMin;
                                        selectedPrefab.GetComponent<RectTransform>().anchorMax = selectedPrefab.asset.GetComponent<RectTransform>().anchorMax;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertComponent += delegate (Component assetComponent)
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert any changes to the component on this instance!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefab = selectedTarget.GetComponent<uPrefab>();

                                    if (selectedPrefab)
                                    {
                                        Component instanceComponent = (selectedPrefab.GetComponent(assetComponent.GetType())) ? selectedPrefab.GetComponent(assetComponent.GetType()) : selectedPrefab.gameObject.AddComponent(assetComponent.GetType());

                                        if (instanceComponent != null)
                                        {
                                            uPrefabUtility.RevertComponentWithProcessor(selectedPrefab.asset.gameObject,  selectedPrefab.gameObject, assetComponent, instanceComponent);
                                        }


                                        uPrefabApplyUtilities.ApplyUp(selectedPrefab);
                                    }
                                }

                                GUIUtility.ExitGUI();

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        PopupWindow.Show(activatorRect, revertPopupWindow);
                    }
                    #endregion

                    #region Apply Button
                    bool didClickApply = GUILayout.Button(new GUIContent("Apply"), EditorStyles.miniButtonRight);

                    if (Event.current.type == EventType.Repaint)
                    {
                        activatorRect = GUILayoutUtility.GetLastRect();
                    }

                    if (didClickApply)
                    {
                        uPrefabApplyPopupWindow popupWindow = new uPrefabApplyPopupWindow();
                        popupWindow.target = prefab.gameObject;
                        popupWindow.modifiedComponents = uPrefabUtility.GetModifiedComponents(prefab.gameObject, prefab.asset.gameObject);
                        popupWindow.showUnmodifiedComponents = uPrefabMenuItems.showUnmodifiedComponents;

                        popupWindow.onApplySelf += delegate
                        {
                            uPrefabApplyUtilities.ApplySelf(prefab);
                            uPrefabApplyUtilities.ApplyUp(prefab);

                            GUIUtility.ExitGUI();

                            EditorApplication.RepaintHierarchyWindow();


                        };

                        popupWindow.onApplyHierarchy += delegate
                        {
                            uPrefabApplyUtilities.ApplyUp(prefab);

                            EditorApplication.RepaintHierarchyWindow();
                        };

                        popupWindow.onApplyComponent += delegate (Component component)
                        {
                            Component assetComponent = prefab.asset.GetComponent(component.GetType());

                            if(!assetComponent)
                            {
                                assetComponent = prefab.asset.gameObject.AddComponent(component.GetType());
                            }

                            uPrefabUtility.ApplyComponentWithProcessor(prefab.asset.gameObject, prefab.gameObject, assetComponent, component);
                            uPrefabApplyUtilities.ApplyUp(prefab);

                            EditorApplication.RepaintHierarchyWindow();

                            if (EditorUtility.DisplayDialog("Revert Instances?", "Do you wish to revert other nested prefab instances component?", "Yes, Revert", "No"))
                            {
                                uPrefabRevertUtilities.RevertComponentOnAllObjectInstances(prefab.asset, assetComponent);
                            }
                        };

                        PopupWindow.Show(activatorRect, popupWindow);
                    }

                    #endregion
                }
                else
                {
                    GUILayout.BeginHorizontal();

                    #region Asset Revert Button
                    bool didClickRevert = GUILayout.Button(new GUIContent("Revert"), EditorStyles.miniButton);

                    if (Event.current.type == EventType.Repaint)
                    {
                        activatorRect = GUILayoutUtility.GetLastRect();
                    }

                    if (didClickRevert)
                    {
                        uPrefabRevertPopupWindow revertPopupWindow = new uPrefabRevertPopupWindow();
                        revertPopupWindow.target = prefab.gameObject;
                        revertPopupWindow.showUnmodifiedComponents = true;

                        revertPopupWindow.onRevertObject += delegate
                           {
                               if (EditorUtility.DisplayDialog("Are you sure?", "This will replace instances with this prefab.\n\nWarning: Any script references will become null!", "Revert", "Cancel"))
                               {
                                   uPrefabRevertUtilities.RevertAllObjectInstances(prefab);

                                   GUIUtility.ExitGUI();

                                   EditorApplication.RepaintHierarchyWindow();
                               }
                           };


                        revertPopupWindow.onRevertComponent += delegate (Component component)
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert changes on other prefab children!", "Revert", "Cancel"))
                            {
                                uPrefabRevertUtilities.RevertComponentOnAllObjectInstances(prefab, component);
                            }
                        };

                        revertPopupWindow.onRevertPosition += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, true, false, false, false, false, false, false);
                        };

                        revertPopupWindow.onRevertRotation += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, false, true, false, false, false, false, false);
                        };

                        revertPopupWindow.onRevertScale += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, false, false, true, false, false, false, false);
                        };

                        revertPopupWindow.onRevertAnchoredPosition += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, false, false, false, true, false, false, false);
                        };

                        revertPopupWindow.onRevertOffset += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, false, false, false, false, true, false, false);
                        };

                        revertPopupWindow.onRevertPivot += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, false, false, false, false, false, true, false);
                        };

                        revertPopupWindow.onRevertAnchors += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefab, false, false, false, false, false, false, true);
                        };

                        PopupWindow.Show(activatorRect, revertPopupWindow);
                    }
                    #endregion

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                GUILayout.Label("This GameObject is not an instance of a prefab.");
                GUILayout.EndHorizontal();
            }
        }

        private void OnDrawPrefabChild(uPrefabChild prefabChild)
        {
            if (prefabChild.asset)
            {
                GUILayout.BeginHorizontal();

                if (!prefabChild.isAsset)
                {
                    if (GUILayout.Button(new GUIContent("Select"), EditorStyles.miniButtonLeft))
                    {
                        Selection.activeGameObject = prefabChild.asset.gameObject;
                    }

                    #region Revert Button
                    bool didClickRevert = GUILayout.Button(new GUIContent("Revert"), EditorStyles.miniButtonMid);

                    if (Event.current.type == EventType.Repaint)
                    {
                        activatorRect = GUILayoutUtility.GetLastRect();
                    }

                    if (didClickRevert)
                    {
                        uPrefabRevertPopupWindow revertPopupWindow = new uPrefabRevertPopupWindow();
                        revertPopupWindow.target = prefabChild.asset.gameObject;
                        revertPopupWindow.modifiedComponents = uPrefabUtility.GetModifiedComponents(prefabChild.gameObject, prefabChild.asset.gameObject);
                        revertPopupWindow.showUnmodifiedComponents = uPrefabMenuItems.showUnmodifiedComponents;

                        revertPopupWindow.onRevertObject += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert all local changes on this prefab child!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(selectedPrefabChild.gameObject.scene);

                                        uPrefabRevertUtilities.RevertAllOnObject(selectedPrefabChild);
                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }

                                GUIUtility.ExitGUI();
                            }
                        };


                        revertPopupWindow.onRevertPosition += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance position!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.transform.localPosition = selectedPrefabChild.asset.transform.position;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }
                            }
                        };

                        revertPopupWindow.onRevertRotation += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance rotation!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.transform.rotation = selectedPrefabChild.asset.transform.rotation;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }
                            }
                        };

                        revertPopupWindow.onRevertScale += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance scale!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.transform.localScale = selectedPrefabChild.asset.transform.localScale;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }
                            }
                        };

                        revertPopupWindow.onRevertAnchoredPosition += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance anchor position!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.GetComponent<RectTransform>().anchoredPosition3D = selectedPrefabChild.asset.GetComponent<RectTransform>().anchoredPosition3D;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertOffset += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance offset!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.GetComponent<RectTransform>().offsetMin = selectedPrefabChild.asset.GetComponent<RectTransform>().offsetMin;
                                        selectedPrefabChild.GetComponent<RectTransform>().offsetMax = selectedPrefabChild.asset.GetComponent<RectTransform>().offsetMax;

                                        uPrefabApplyUtilities.ApplyUp(prefabChild.uPrefabParent);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };


                        revertPopupWindow.onRevertPivot += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance pivot!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.GetComponent<RectTransform>().pivot = selectedPrefabChild.asset.GetComponent<RectTransform>().pivot;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);

                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertAnchors += delegate
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert the instance anchors!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        selectedPrefabChild.GetComponent<RectTransform>().anchorMin = selectedPrefabChild.asset.GetComponent<RectTransform>().anchorMin;
                                        selectedPrefabChild.GetComponent<RectTransform>().anchorMax = selectedPrefabChild.asset.GetComponent<RectTransform>().anchorMax;

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }

                                EditorApplication.RepaintHierarchyWindow();
                            }
                        };

                        revertPopupWindow.onRevertComponent += delegate (Component assetComponent)
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert any changes to the component on this instance!", "Revert", "Cancel"))
                            {
                                foreach (var selectedTarget in Selection.gameObjects)
                                {
                                    var selectedPrefabChild = selectedTarget.GetComponent<uPrefabChild>();

                                    if (selectedPrefabChild)
                                    {
                                        Component instanceComponent = selectedPrefabChild.GetComponent(assetComponent.GetType()) ? selectedPrefabChild.GetComponent(assetComponent.GetType()) : selectedPrefabChild.gameObject.AddComponent(assetComponent.GetType());

                                        if (instanceComponent != null)
                                        {
                                            uPrefabUtility.RevertComponentWithProcessor(selectedPrefabChild.uPrefabParent.asset.gameObject,  selectedPrefabChild.uPrefabParent.gameObject, assetComponent, instanceComponent);
                                        }

                                        uPrefabApplyUtilities.ApplyUp(selectedPrefabChild.uPrefabParent);
                                    }
                                }

                                GUIUtility.ExitGUI();
                            }
                        };

                        PopupWindow.Show(activatorRect, revertPopupWindow);
                    }
                    #endregion

                    #region Apply Button
                    bool didClickApply = GUILayout.Button(new GUIContent("Apply"), EditorStyles.miniButtonRight);

                    if (Event.current.type == EventType.Repaint)
                    {
                        activatorRect = GUILayoutUtility.GetLastRect();
                    }

                    if (didClickApply)
                    {
                        uPrefabApplyPopupWindow popupWindow = new uPrefabApplyPopupWindow();
                        popupWindow.target = prefabChild.gameObject;
                        popupWindow.modifiedComponents = uPrefabUtility.GetModifiedComponents(prefabChild.gameObject, prefabChild.asset.gameObject);
                        popupWindow.showUnmodifiedComponents = uPrefabMenuItems.showUnmodifiedComponents;

                        popupWindow.onApplySelf += delegate
                        {
                            PrefabUtility.ReplacePrefab(prefabChild.uPrefabParent.gameObject, prefabChild.uPrefabParent.asset.gameObject, ReplacePrefabOptions.ConnectToPrefab);
                            uPrefabApplyUtilities.ApplyUp(prefabChild.uPrefabParent);

                            GUIUtility.ExitGUI();

                            EditorApplication.RepaintHierarchyWindow();
                        };

                        popupWindow.onApplyHierarchy += delegate
                        {
                            uPrefabApplyUtilities.ApplyUp(prefabChild.uPrefabParent);
                        };

                        popupWindow.onApplyComponent += delegate (Component component)
                        {
                            Component assetComponent = prefabChild.asset.GetComponent(component.GetType());

                            if (!assetComponent)
                            {
                                assetComponent = prefabChild.asset.gameObject.AddComponent(component.GetType());
                            }

                            uPrefabUtility.ApplyComponentWithProcessor(prefabChild.asset.gameObject, prefabChild.gameObject, assetComponent, component);
                            uPrefabApplyUtilities.ApplyUp(prefabChild.uPrefabParent);

                            if (EditorUtility.DisplayDialog("Revert Instances?", "Do you wish to revert other nested prefab instances component?", "Yes, Revert", "No"))
                            {
                                uPrefabRevertUtilities.RevertComponentOnAllObjectInstances(prefabChild.asset, assetComponent);
                            }

                            EditorApplication.RepaintHierarchyWindow();
                        };

                        PopupWindow.Show(activatorRect, popupWindow);
                    }

                    #endregion

                }
                else
                {
                    bool didClickRevert = GUILayout.Button(new GUIContent("Revert"), EditorStyles.miniButton);

                    if (Event.current.type == EventType.Repaint)
                    {
                        activatorRect = GUILayoutUtility.GetLastRect();
                    }

                    if (didClickRevert)
                    {
                        uPrefabRevertPopupWindow revertPopupWindow = new uPrefabRevertPopupWindow();
                        revertPopupWindow.target = prefabChild.gameObject;
                        revertPopupWindow.showUnmodifiedComponents = true;

                        revertPopupWindow.onRevertObject += delegate
                           {
                               if (EditorUtility.DisplayDialog("Are you sure?", "This will revert all other instances of this child prefab!", "Revert", "Cancel"))
                               {
                                   uPrefabRevertUtilities.RevertAllObjectInstances(prefabChild);

                                   GUIUtility.ExitGUI();

                                   EditorApplication.RepaintHierarchyWindow();
                               }
                           };


                        revertPopupWindow.onRevertComponent += delegate (Component component)
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?", "This will revert changes on other prefab children!", "Revert", "Cancel"))
                            {
                                uPrefabRevertUtilities.RevertComponentOnAllObjectInstances(prefabChild, component);
                            }
                        };

                        revertPopupWindow.onRevertPosition += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, true, false, false, false, false, false, false);
                        };

                        revertPopupWindow.onRevertRotation += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, false, true, false, false, false, false, false);
                        };

                        revertPopupWindow.onRevertScale += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, false, false, true, false, false, false, false); ;
                        };

                        revertPopupWindow.onRevertAnchoredPosition += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, false, false, false, true, false, false, false);
                        };

                        revertPopupWindow.onRevertOffset += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, false, false, false, false, true, false, false);
                        };

                        revertPopupWindow.onRevertPivot += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, false, false, false, false, false, true, false);
                        };

                        revertPopupWindow.onRevertAnchors += delegate
                        {
                            uPrefabRevertUtilities.RevertTransformOnAllObjectInstances(prefabChild, false, false, false, false, false, false, true);
                        };

                        PopupWindow.Show(activatorRect, revertPopupWindow);
                    }
                }

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                GUILayout.Label("This is not a child of a prefab.");
                GUILayout.EndHorizontal();
            }
        }
    }
}