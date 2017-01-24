using Assets.Scripts.GameObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Parts
{
    public class HandleSlideArea : MonoBehaviour
    {
        public Image ImageRenderer;

        void OnEnable()
        {
            gameObject.AddComponent<RectTransform>();
            ImageRenderer = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();
            ImageRenderer.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            ImageRenderer.type = Image.Type.Simple;
        }
    }
}
