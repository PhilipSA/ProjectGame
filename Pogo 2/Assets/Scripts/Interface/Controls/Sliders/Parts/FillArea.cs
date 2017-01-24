using Assets.Scripts.GameObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Sliders.Parts
{
    public class FillArea : MonoBehaviour
    {
        public Image ImageRenderer;

        void OnEnable()
        {
            gameObject.AddComponent<RectTransform>();
            ImageRenderer = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();
            ImageRenderer.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            ImageRenderer.type = Image.Type.Sliced;
        }
    }
}
