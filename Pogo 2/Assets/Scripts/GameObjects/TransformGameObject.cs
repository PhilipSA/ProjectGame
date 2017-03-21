using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public class TransformGameObject
    {
        public static Graphic SetAnchors(Graphic gameObject, Vector2 anchorMin, Vector2 anchorMax)
        {
            gameObject.rectTransform.anchorMin = anchorMin;
            gameObject.rectTransform.anchorMax = anchorMax;
            return gameObject;
        }

        public static RectTransform SetAnchors(RectTransform gameObject, Vector2 anchorMin, Vector2 anchorMax)
        {
            gameObject.anchorMin = anchorMin;
            gameObject.anchorMax = anchorMax;
            return gameObject;
        }
    }
}
