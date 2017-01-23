using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class CreateGameObject
    {
        public static GameObject CreateChildGameObject<T>(Transform parent) where T : Component
        {
            GameObject child = new GameObject(typeof(T).Name, typeof(T));
            child.transform.SetParent(parent, false);
            return child;
        }
    }
}
