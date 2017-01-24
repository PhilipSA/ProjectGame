using Assets.Scripts.CustomComponents;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class TimerDisplay : MonoBehaviour
    {
        public StopWatch StopWatch;
        private Text _text;
        private Image _image;
        private Canvas _canvas;

        // Use this for initialization
        void Start()
        {
            _canvas = gameObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            _image = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();
            _image.rectTransform.anchorMax = Vector2.up;
            _image.rectTransform.anchorMin = Vector2.up;
            _image.transform.localPosition = new Vector2(-400, 200);

            _image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
             var color = Color.black;
             color.a = 0.5f;
            _image.color = color;
            _image.type = Image.Type.Sliced;

            _text = CreateGameObject.CreateChildGameObject<ControlText>(_image.transform).GetComponent<ControlText>();   
            _text.color = Color.white;
            _text.fontSize = 25;

            StopWatch = new StopWatch(true);
        }

        // Update is called once per frame
        void Update()
        {
            StopWatch.Tick();
            _text.text = StopWatch.GetTimeInMmssffFormat();
        }

        public void StopTimer()
        {
            StopWatch.Enabled = false;
        }

        void OnGui()
        {

        }
    }
}
