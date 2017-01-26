using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class CreateGameObject
    {
        public static GameObject CreateChildGameObject<T>(Transform parent, string name = null) where T : Component
        {
            GameObject child = name == null ? new GameObject(typeof(T).Name, typeof(T)) : new GameObject(name, typeof(T));
            child.transform.SetParent(parent, false);
            
            return child;
        }
    }
}
