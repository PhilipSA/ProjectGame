using Assets.Scripts.GameObjects.Components.Controls.Abstraction;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Controls.Text
{
    public class ControlText : UnityEngine.UI.Text, ILocalizableControl
    {
        public string DisplayText { get; set; }

        protected override void Start()
        {
            color = Color.black;
            font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            fontSize = 14;
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
