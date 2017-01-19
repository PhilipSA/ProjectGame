using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(Text))]
    public class uPrefabTextProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("onCullStateChanged");
                names.Add("text");
                names.Add("m_Text");

                return names;
            }
        }

        public override void OnRevertComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            Text targetText = (Text)instanceComponent;

            // Save target text...
            string textToReset = targetText.text;

            base.OnRevertComponent(componentType, asset, instance, assetComponent, instanceComponent);

            // Restore target text...
            targetText.text = textToReset;
        }
    }
}