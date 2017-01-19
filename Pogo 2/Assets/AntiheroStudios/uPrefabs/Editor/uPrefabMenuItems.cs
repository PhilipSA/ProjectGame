using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    public class uPrefabMenuItems
    {
        static string kVersion = "1.6f7";

        public static Color modifiedColor
        {
            get
            {
                string color = EditorPrefs.GetString("uPrefabs.modifiedColor", JsonUtility.ToJson(Color.black));

                return JsonUtility.FromJson<Color>(color);
            }
            set
            {
                EditorPrefs.SetString("uPrefabs.modifiedColor", JsonUtility.ToJson(value));
            }
        }

        public static Color missingColor
        {
            get
            {
                string color = EditorPrefs.GetString("uPrefabs.missingColor", JsonUtility.ToJson(Color.red));

                return JsonUtility.FromJson<Color>(color);
            }
            set
            {
                EditorPrefs.SetString("uPrefabs.missingColor", JsonUtility.ToJson(value));
            }
        }

        public static Color addedColor
        {
            get
            {
                string color = EditorPrefs.GetString("uPrefabs.addedColor", JsonUtility.ToJson(Color.green));

                return JsonUtility.FromJson<Color>(color);
            }
            set
            {
                EditorPrefs.SetString("uPrefabs.addedColor", JsonUtility.ToJson(value));
            }
        }

        public static bool showUnmodifiedComponents
        {
            get
            {
                return EditorPrefs.GetBool("uPrefabs.showUnmodifiedComponents", false);
            }
            set
            {
                EditorPrefs.SetBool("uPrefabs.showUnmodifiedComponents", value);
            }
        }

        [MenuItem("Window/Antihero Studios/uPrefabs/Wiki", false, 0)]
        private static void OnlineWiki()
        {
            Application.OpenURL("https://bitbucket.org/antiherostudios/uprefabs/wiki/Home");
        }

        [MenuItem("Window/Antihero Studios/uPrefabs/Report an Issue", false, 1)]
        private static void ReportAnIssue()
        {
            Application.OpenURL("https://bitbucket.org/antiherostudios/uprefabs/issues?status=new&status=open");
        }

        [MenuItem("Window/Antihero Studios/uPrefabs/Uninstall Components", false, 100)]
        private static void Uninstall()
        {
            if (EditorUtility.DisplayDialog("Are you sure?", "This will remove all uPrefab and uPrefabChild components from prefabs in your project!", "Remove", "Cancel"))
            {
                var prefabs = uPrefabUtility.FindAllPrefabObjects();

                foreach (var prefab in prefabs)
                {
                    uPrefab[] uPrefabComponents = prefab.GetComponentsInChildren<uPrefab>(true);
                    uPrefabChild[] uPrefabChildComponents = prefab.GetComponentsInChildren<uPrefabChild>(true);

                    for (int i = uPrefabComponents.Length - 1; i >= 0; i--)
                    {
                        Object.DestroyImmediate(uPrefabComponents[i], true);
                    }

                    for (int i = uPrefabChildComponents.Length - 1; i >= 0; i--)
                    {
                        Object.DestroyImmediate(uPrefabChildComponents[i], true);
                    }
                }

                EditorUtility.DisplayDialog("Safe to Delete", "It is now safe to delete the uPrefabs source code folder.", "Continue");
            }
        }

        [PreferenceItem("uPrefab")]
        private static void PreferencesMenu()
        {
            GUILayout.Label("Colors", EditorStyles.boldLabel);
            var modColor = EditorGUILayout.ColorField("Modified Color", modifiedColor);
            var misColor = EditorGUILayout.ColorField("Missing Color", missingColor);
            var addColor = EditorGUILayout.ColorField("Added Color", addedColor);

            GUILayout.Label("Apply & Revert Windows", EditorStyles.boldLabel);
            var showUnmodRev = EditorGUILayout.Toggle("Show Unmodified Components", showUnmodifiedComponents);

            GUILayout.FlexibleSpace();
            GUILayout.Label("uPrefabs " + kVersion, EditorStyles.centeredGreyMiniLabel);

            if (GUI.changed)
            {
                modifiedColor = modColor;
                missingColor = misColor;
                addedColor = addColor;
                showUnmodifiedComponents = showUnmodRev;
            }
        }
    }
}