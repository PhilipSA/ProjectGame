using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(Button), true)]
    public class uPrefabButtonProcessor : uPrefabComponentProcessor
    {
        public override System.Collections.Generic.List<string> SkipSerializedPropertyNames
        {
            get
            {
                var names = base.SkipSerializedPropertyNames;

                names.Add("onClick");
                names.Add("animationTriggers");

                return names;
            }
        }

        public override void OnRevertComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            Button targetBtn = (Button)instanceComponent;

            var onClick = targetBtn.onClick;

            base.OnRevertComponent(componentType, asset, instance, assetComponent, instanceComponent);

            targetBtn.onClick = onClick;
        }
    }
}