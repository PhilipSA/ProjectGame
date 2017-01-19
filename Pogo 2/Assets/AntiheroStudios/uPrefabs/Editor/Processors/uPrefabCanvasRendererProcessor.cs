using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(CanvasRenderer), true)]
    public class uPrefabCanvasRendererProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("onCullStateChanged");
                names.Add("materialCount");

                return names;
            }
        }
    }
}