using GameObjects.Components.Abstraction;
using UnityEngine;

namespace GameObjects.Components.Image
{
    public class CustomImage : UnityEngine.UI.Image, IRectTransformAble
    {
        protected override void Awake()
        {
           rectTransform.sizeDelta = new Vector2(0, 0);
        }

        public void Initialize(Sprite imageSprite, Type imageType, Vector2 anchorMin, Vector2 anchorMax)
        {
            sprite = imageSprite;
            type = imageType;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        public void Initialize(Sprite imageSprite, Type imageType)
        {
            sprite = imageSprite;
            type = imageType;
        }

        public void SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        public void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.pivot = pivot;
        }
    }
}
