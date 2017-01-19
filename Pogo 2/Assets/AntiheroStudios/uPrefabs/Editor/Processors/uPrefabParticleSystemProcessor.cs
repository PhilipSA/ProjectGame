using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace AntiheroStudios.uPrefabs.Editor
{
    [uPrefabComponentHandler(typeof(ParticleSystem))]
    public class uPrefabParticleSystemProcessor : uPrefabComponentProcessor
    {
        public override void OnRevertComponent(Type componentType, GameObject asset, GameObject instance, Component assetComponent, Component instanceComponent)
        {
            UnityEditor.EditorUtility.CopySerialized(assetComponent, instanceComponent);
        }
    }
}