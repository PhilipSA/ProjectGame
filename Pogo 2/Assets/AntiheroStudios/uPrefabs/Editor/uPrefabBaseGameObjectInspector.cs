using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AntiheroStudios.uPrefabs.Editor
{
    public class uPrefabBaseGameObjectInspector : UnityEditor.Editor
    {
        private static GameObject dragObject;
        private List<GameObject> m_PreviewInstances;
        private PreviewRenderUtility m_PreviewUtility;
        private SerializedProperty m_StaticEditorFlags;
        private Vector2 previewDir;
        private FieldInfo m_ignoreRaySnapField;
        private MethodInfo m_calcRayOffsetMethod;

        private GameObject gameObject
        {
            get
            {
                return target as GameObject;
            }
        }

        private GUIStyle staticDropdown
        {
            get
            {
                return GUI.skin.FindStyle("StaticDropDown");
            }
        }

        protected override void OnHeaderGUI()
        {
            //base.OnHeaderGUI();
        }

        public override void OnInspectorGUI()
        {
            this.OnDrawGameObjectHeader();

            if (!Application.isPlaying)
            {
                if (Selection.gameObjects.Length > 0)
                {
                    this.OnDrawPrefabButtons();
                }
            }
            else
            {
                GUILayout.Label("Cannot modify Prefabs while in Play Mode", EditorStyles.helpBox);
            }
        }

        private int referenceTargetIndex
        {
            get
            {
                return (int)typeof(UnityEditor.Editor).GetProperty("referenceTargetIndex", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        private static bool ShowMixedStaticEditorFlags(StaticEditorFlags mask)
        {
            uint num = 0u;
            uint num2 = 0u;

            foreach (object current in Enum.GetValues(typeof(StaticEditorFlags)))
            {
                num2 += 1u;
                if ((mask & (StaticEditorFlags)((int)current)) > (StaticEditorFlags)0)
                {
                    num += 1u;
                }
            }
            return num > 0u && num != num2;
        }

        private void OnDrawGameObjectHeader()
        {
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();

            SerializedProperty m_Icon = serializedObject.FindProperty("m_Icon");
            GUIContent goIcon = EditorGUIUtility.IconContent("GameObject Icon");

            typeof(EditorGUI).GetMethod("ObjectIconDropDown", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { new Rect(3f, 4f, 24f, 24f), base.targets, true, goIcon.image as Texture2D, m_Icon });

            GUILayout.Space(20);

            var isActive = EditorGUILayout.Toggle(gameObject.activeSelf, GUILayout.Width(15));

            if (isActive != gameObject.activeSelf)
            {
                foreach (var gObj in Selection.gameObjects)
                {
                    gObj.SetActive(isActive);
                }
            }

            var gName = EditorGUILayout.TextField(gameObject.name, GUILayout.MinWidth(125));

            if (gName != gameObject.name && !string.IsNullOrEmpty(gName))
            {
                foreach (var gObj in Selection.gameObjects)
                {
                    gObj.name = gName;
                }
            }

            var gStaticToggle = EditorGUILayout.ToggleLeft("Static", gameObject.isStatic, GUILayout.Width(45));

            foreach (var gObj in Selection.gameObjects)
            {
                if (gObj.isStatic != gStaticToggle)
                {
                    gObj.isStatic = gStaticToggle;
                }
            }

            var gStatic = (StaticEditorFlags)EditorGUILayout.EnumMaskField(GameObjectUtility.GetStaticEditorFlags(gameObject), staticDropdown, GUILayout.Width(10), GUILayout.Height(14));

            foreach (var gObj in Selection.gameObjects)
            {
                var oStatic = GameObjectUtility.GetStaticEditorFlags(gObj);

                if (oStatic != gStatic)
                {
                    GameObjectUtility.SetStaticEditorFlags(gObj, gStatic);
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(2);

            GUILayout.BeginHorizontal();

            GUILayout.Space(12);

            GUILayout.Label("Tag", GUILayout.Width(35));
            var gTag = EditorGUILayout.TagField(gameObject.tag);

            if (gTag != gameObject.tag)
            {
                foreach (var gObj in Selection.gameObjects)
                {
                    gObj.tag = gTag;
                }
            }

            GUILayout.Label("Layer", GUILayout.Width(35));
            var gLayer = EditorGUILayout.LayerField(gameObject.layer);

            if (gLayer != gameObject.layer)
            {
                if (gameObject.transform.childCount > 0 && EditorUtility.DisplayDialog("Assign Recursively?", "Do you want to assign the children layers as well?", "Assign Children Layers", "Only this GameObject"))
                {
                    foreach (var gObj in Selection.gameObjects)
                    {
                        SetLayerRecursively(gObj, gLayer);
                    }
                }
                else
                {
                    foreach (var gObj in Selection.gameObjects)
                    {
                        gObj.layer = gLayer;
                    }
                }
            }

            GUILayout.EndHorizontal();
        }

        private void SetLayerRecursively(GameObject tGameObject, int layerMask)
        {
            tGameObject.layer = layerMask;

            foreach (Transform child in tGameObject.transform)
            {
                SetLayerRecursively(child.gameObject, layerMask);
            }
        }

        protected virtual void OnDrawPrefabButtons()
        {

        }

        public override void ReloadPreviewInstances()
        {
            this.CreatePreviewInstances();
        }

        private void DestroyPreviewInstances()
        {
            if (this.m_PreviewInstances == null || this.m_PreviewInstances.Count == 0)
            {
                return;
            }
            foreach (GameObject current in this.m_PreviewInstances)
            {
                UnityEngine.Object.DestroyImmediate(current);
            }
            this.m_PreviewInstances.Clear();
        }

        public static void SetEnabledRecursive(GameObject go, bool enabled)
        {
            Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                Renderer renderer = componentsInChildren[i];
                renderer.enabled = enabled;
            }
        }

        private void CreatePreviewInstances()
        {
            this.DestroyPreviewInstances();
            if (this.m_PreviewInstances == null)
            {
                this.m_PreviewInstances = new List<GameObject>(base.targets.Length);
            }
            for (int i = 0; i < base.targets.Length; i++)
            {
                GameObject gameObject = (GameObject)typeof(EditorUtility).GetMethod("InstantiateForAnimatorPreview", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { base.targets[i] });
                SetEnabledRecursive(gameObject, false);
                this.m_PreviewInstances.Add(gameObject);
            }
        }

        private void InitPreview()
        {
            if (this.m_PreviewUtility == null)
            {
                this.m_PreviewUtility = new PreviewRenderUtility(true);
                this.m_PreviewUtility.m_CameraFieldOfView = 30f;
                this.m_PreviewUtility.m_Camera.cullingMask = 1 << (int)typeof(Camera).GetProperty("PreviewCullingLayer", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null);
                this.CreatePreviewInstances();
            }
        }

        public void OnDestroy()
        {
            this.DestroyPreviewInstances();

            if (this.m_PreviewUtility != null)
            {
                this.m_PreviewUtility.Cleanup();
                this.m_PreviewUtility = null;
            }
        }

        public static bool HasRenderablePartsRecurse(GameObject go)
        {
            MeshRenderer exists = go.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
            MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
            if (exists && meshFilter && meshFilter.sharedMesh)
            {
                return true;
            }
            SkinnedMeshRenderer skinnedMeshRenderer = go.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
            if (skinnedMeshRenderer && skinnedMeshRenderer.sharedMesh)
            {
                return true;
            }
            SpriteRenderer spriteRenderer = go.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            if (spriteRenderer && spriteRenderer.sprite)
            {
                return true;
            }
            foreach (Transform transform in go.transform)
            {
                if (HasRenderablePartsRecurse(transform.gameObject))
                {
                    return true;
                }
            }
            return false;
        }

        public static void GetRenderableBoundsRecurse(ref Bounds bounds, GameObject go)
        {
            MeshRenderer meshRenderer = go.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
            MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
            if (meshRenderer && meshFilter && meshFilter.sharedMesh)
            {
                if (bounds.extents == Vector3.zero)
                {
                    bounds = meshRenderer.bounds;
                }
                else
                {
                    bounds.Encapsulate(meshRenderer.bounds);
                }
            }
            SkinnedMeshRenderer skinnedMeshRenderer = go.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
            if (skinnedMeshRenderer && skinnedMeshRenderer.sharedMesh)
            {
                if (bounds.extents == Vector3.zero)
                {
                    bounds = skinnedMeshRenderer.bounds;
                }
                else
                {
                    bounds.Encapsulate(skinnedMeshRenderer.bounds);
                }
            }
            SpriteRenderer spriteRenderer = go.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            if (spriteRenderer && spriteRenderer.sprite)
            {
                if (bounds.extents == Vector3.zero)
                {
                    bounds = spriteRenderer.bounds;
                }
                else
                {
                    bounds.Encapsulate(spriteRenderer.bounds);
                }
            }
            foreach (Transform transform in go.transform)
            {
                GetRenderableBoundsRecurse(ref bounds, transform.gameObject);
            }
        }

        private static float GetRenderableCenterRecurse(ref Vector3 center, GameObject go, int depth, int minDepth, int maxDepth)
        {
            if (depth > maxDepth)
            {
                return 0f;
            }
            float num = 0f;
            if (depth > minDepth)
            {
                MeshRenderer meshRenderer = go.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
                MeshFilter x = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
                SkinnedMeshRenderer skinnedMeshRenderer = go.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
                SpriteRenderer spriteRenderer = go.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                if (meshRenderer == null && x == null && skinnedMeshRenderer == null && spriteRenderer == null)
                {
                    num = 1f;
                    center += go.transform.position;
                }
                else
                {
                    if (meshRenderer != null && x != null)
                    {
                        if (Vector3.Distance(meshRenderer.bounds.center, go.transform.position) < 0.01f)
                        {
                            num = 1f;
                            center += go.transform.position;
                        }
                    }
                    else
                    {
                        if (skinnedMeshRenderer != null)
                        {
                            if (Vector3.Distance(skinnedMeshRenderer.bounds.center, go.transform.position) < 0.01f)
                            {
                                num = 1f;
                                center += go.transform.position;
                            }
                        }
                        else
                        {
                            if (spriteRenderer != null && Vector3.Distance(spriteRenderer.bounds.center, go.transform.position) < 0.01f)
                            {
                                num = 1f;
                                center += go.transform.position;
                            }
                        }
                    }
                }
            }
            depth++;
            foreach (Transform transform in go.transform)
            {
                num += GetRenderableCenterRecurse(ref center, transform.gameObject, depth, minDepth, maxDepth);
            }
            return num;
        }

        public static Vector3 GetRenderableCenterRecurse(GameObject go, int minDepth, int maxDepth)
        {
            Vector3 vector = Vector3.zero;
            float renderableCenterRecurse = GetRenderableCenterRecurse(ref vector, go, 0, minDepth, maxDepth);
            if (renderableCenterRecurse > 0f)
            {
                vector /= renderableCenterRecurse;
            }
            else
            {
                vector = go.transform.position;
            }
            return vector;
        }

        public override bool HasPreviewGUI()
        {
            return EditorUtility.IsPersistent(this.target) && this.HasStaticPreview();
        }

        private bool HasStaticPreview()
        {
            if (base.targets.Length > 1)
            {
                return true;
            }
            if (this.target == null)
            {
                return false;
            }
            GameObject gameObject = this.target as GameObject;
            Camera exists = gameObject.GetComponent(typeof(Camera)) as Camera;
            return exists || HasRenderablePartsRecurse(gameObject);
        }

        private void DoRenderPreview()
        {
            GameObject gameObject = this.m_PreviewInstances[this.referenceTargetIndex];
            Bounds bounds = new Bounds(gameObject.transform.position, Vector3.zero);
            GetRenderableBoundsRecurse(ref bounds, gameObject);
            float num = Mathf.Max(bounds.extents.magnitude, 0.0001f);
            float num2 = num * 3.8f;
            Quaternion quaternion = Quaternion.Euler(-this.previewDir.y, -this.previewDir.x, 0f);
            Vector3 position = bounds.center - quaternion * (Vector3.forward * num2);
            this.m_PreviewUtility.m_Camera.transform.position = position;
            this.m_PreviewUtility.m_Camera.transform.rotation = quaternion;
            this.m_PreviewUtility.m_Camera.nearClipPlane = num2 - num * 1.1f;
            this.m_PreviewUtility.m_Camera.farClipPlane = num2 + num * 1.1f;
            this.m_PreviewUtility.m_Light[0].intensity = 0.7f;
            this.m_PreviewUtility.m_Light[0].transform.rotation = quaternion * Quaternion.Euler(40f, 40f, 0f);
            this.m_PreviewUtility.m_Light[1].intensity = 0.7f;
            this.m_PreviewUtility.m_Light[1].transform.rotation = quaternion * Quaternion.Euler(340f, 218f, 177f);
            Color ambient = new Color(0.1f, 0.1f, 0.1f, 0f);
            InternalEditorUtility.SetCustomLighting(this.m_PreviewUtility.m_Light, ambient);
            bool fog = RenderSettings.fog;
            Unsupported.SetRenderSettingsUseFogNoDirty(false);
            SetEnabledRecursive(gameObject, true);
            this.m_PreviewUtility.m_Camera.Render();
            SetEnabledRecursive(gameObject, false);
            Unsupported.SetRenderSettingsUseFogNoDirty(fog);
            InternalEditorUtility.RemoveCustomLighting();
        }


        public static Vector2 Drag2D(Vector2 scrollPosition, Rect position)
        {
            int controlID = GUIUtility.GetControlID("Slider".GetHashCode(), FocusType.Passive);
            Event current = Event.current;
            switch (current.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                    if (position.Contains(current.mousePosition) && position.width > 50f)
                    {
                        GUIUtility.hotControl = controlID;
                        current.Use();
                        EditorGUIUtility.SetWantsMouseJumping(1);
                    }
                    break;
                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                    }
                    EditorGUIUtility.SetWantsMouseJumping(0);
                    break;
                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlID)
                    {
                        scrollPosition -= current.delta * (float)((!current.shift) ? 1 : 3) / Mathf.Min(position.width, position.height) * 140f;
                        scrollPosition.y = Mathf.Clamp(scrollPosition.y, -90f, 90f);
                        current.Use();
                        GUI.changed = true;
                    }
                    break;
            }
            return scrollPosition;
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            if (!ShaderUtil.hardwareSupportsRectRenderTexture)
            {
                if (Event.current.type == EventType.Repaint)
                {
                    EditorGUI.DropShadowLabel(new Rect(r.x, r.y, r.width, 40f), "Preview requires\nrender texture support");
                }
                return;
            }
            this.InitPreview();
            this.previewDir = Drag2D(this.previewDir, r);
            if (Event.current.type != EventType.Repaint)
            {
                return;
            }
            this.m_PreviewUtility.BeginPreview(r, background);
            this.DoRenderPreview();
            Texture image = this.m_PreviewUtility.EndPreview();
            GUI.DrawTexture(r, image, ScaleMode.StretchToFill, false);
        }


        public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
        {
            if (!this.HasStaticPreview() || !ShaderUtil.hardwareSupportsRectRenderTexture)
            {
                return null;
            }
            this.InitPreview();
            this.m_PreviewUtility.BeginStaticPreview(new Rect(0f, 0f, (float)width, (float)height));
            this.DoRenderPreview();
            return this.m_PreviewUtility.EndStaticPreview();
        }

        public override void OnPreviewSettings()
        {
            if (!ShaderUtil.hardwareSupportsRectRenderTexture)
            {
                return;
            }
            GUI.enabled = true;
            this.InitPreview();
        }

        public Transform[] ignoreRaySnapObjects
        {
            get
            {
                if (m_ignoreRaySnapField == null)
                {
                    m_ignoreRaySnapField = typeof(HandleUtility).GetField("ignoreRaySnapObjects", BindingFlags.Static | BindingFlags.NonPublic);
                }

                return (Transform[])m_ignoreRaySnapField.GetValue(null);
            }
            set
            {
                if (m_ignoreRaySnapField == null)
                {
                    m_ignoreRaySnapField = typeof(HandleUtility).GetField("ignoreRaySnapObjects", BindingFlags.Static | BindingFlags.NonPublic);
                }

                m_ignoreRaySnapField.SetValue(null, value);
            }
        }

        public float CalcRayPlaceOffset(Transform[] objects, Vector3 normal)
        {
            if (m_calcRayOffsetMethod == null)
            {
                m_calcRayOffsetMethod = typeof(HandleUtility).GetMethod("CalcRayPlaceOffset", BindingFlags.Static | BindingFlags.NonPublic);
            }

            return (float)m_calcRayOffsetMethod.Invoke(null, new object[] { objects, normal });
        }

        public void OnSceneDrag(SceneView sceneView)
        {
            GameObject tGameObject = this.target as GameObject;
            PrefabType prefabType = PrefabUtility.GetPrefabType(tGameObject);

            if (prefabType != PrefabType.Prefab && prefabType != PrefabType.ModelPrefab)
            {
                return;
            }

            Event current = Event.current;
            EventType type = current.type;

            if (type != EventType.DragUpdated)
            {
                if (type != EventType.DragPerform)
                {
                    if (type == EventType.DragExited)
                    {
                        if (uPrefabBaseGameObjectInspector.dragObject)
                        {
                            UnityEngine.Object.DestroyImmediate(uPrefabBaseGameObjectInspector.dragObject, false);
                            uPrefabBaseGameObjectInspector.dragObject = null;
                            ignoreRaySnapObjects = null;
                            current.Use();
                        }
                    }
                }
                else
                {
                    string uniqueNameForSibling = GameObjectUtility.GetUniqueNameForSibling(null, uPrefabBaseGameObjectInspector.dragObject.name);
                    uPrefabBaseGameObjectInspector.dragObject.hideFlags = HideFlags.None;
                    Undo.RegisterCreatedObjectUndo(uPrefabBaseGameObjectInspector.dragObject, "Place " + uPrefabBaseGameObjectInspector.dragObject.name);
                    EditorUtility.SetDirty(uPrefabBaseGameObjectInspector.dragObject);
                    DragAndDrop.AcceptDrag();
                    Selection.activeObject = uPrefabBaseGameObjectInspector.dragObject;
                    EditorWindow.mouseOverWindow.Focus();
                    uPrefabBaseGameObjectInspector.dragObject.name = uniqueNameForSibling;
                    uPrefabBaseGameObjectInspector.dragObject = null;
                    ignoreRaySnapObjects = null;
                    current.Use();
                }
            }
            else
            {
                if (uPrefabBaseGameObjectInspector.dragObject == null)
                {
                    uPrefabBaseGameObjectInspector.dragObject = (GameObject)PrefabUtility.InstantiatePrefab(PrefabUtility.FindPrefabRoot(tGameObject));
                    ignoreRaySnapObjects = uPrefabBaseGameObjectInspector.dragObject.GetComponentsInChildren<Transform>();
                    uPrefabBaseGameObjectInspector.dragObject.hideFlags = HideFlags.HideInHierarchy;
                    uPrefabBaseGameObjectInspector.dragObject.name = tGameObject.name;
                }

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                object obj = HandleUtility.RaySnap(HandleUtility.GUIPointToWorldRay(current.mousePosition));

                if (obj != null)
                {
                    RaycastHit raycastHit = (RaycastHit)obj;
                    float d = 0f;

                    if (Tools.pivotMode == PivotMode.Center)
                    {
                        float num = CalcRayPlaceOffset(ignoreRaySnapObjects, raycastHit.normal);

                        if (!float.IsPositiveInfinity(num))
                        {
                            d = Vector3.Dot(uPrefabBaseGameObjectInspector.dragObject.transform.position, raycastHit.normal) - num;
                        }
                    }

                    uPrefabBaseGameObjectInspector.dragObject.transform.position = Matrix4x4.identity.MultiplyPoint(raycastHit.point + raycastHit.normal * d);
                }
                else
                {
                    uPrefabBaseGameObjectInspector.dragObject.transform.position = HandleUtility.GUIPointToWorldRay(current.mousePosition).GetPoint(10f);
                }

                if (sceneView.in2DMode)
                {
                    Vector3 position = uPrefabBaseGameObjectInspector.dragObject.transform.position;
                    position.z = PrefabUtility.FindPrefabRoot(tGameObject).transform.position.z;
                    uPrefabBaseGameObjectInspector.dragObject.transform.position = position;
                }

                current.Use();
            }
        }
    }
}