using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Abstraction
{
    public interface IRectTransformAble
    {
        void SetAnchors(Vector2 anchorMin, Vector2 anchorMax);
        void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot);
    }
}
