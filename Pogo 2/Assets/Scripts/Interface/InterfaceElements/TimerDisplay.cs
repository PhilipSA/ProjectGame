using Assets.Scripts.CustomComponents;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Text;
using Assets.Scripts.Interface.InterfaceElements.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class TimerDisplay : InterfaceElement
    {
        public StopWatch StopWatch;
        private ControlText _text;
        private Image _texture2D;

        // Use this for initialization
        protected override void Awake()
        {
            _texture2D = CreateGameObject.CreateChildGameObject<Image>(transform).GetComponent<Image>();      
            _text = CreateGameObject.CreateChildGameObject<ControlText>(_texture2D.transform).GetComponent<ControlText>();
            StopWatch = new StopWatch(true);
            base.Awake();
        }

        void Start()
        {
            _texture2D.rectTransform.sizeDelta = new Vector2(80, 20);
            _texture2D.rectTransform.anchorMin = new Vector2(0, 1);
            _texture2D.rectTransform.anchorMax = new Vector2(0, 1);
            _texture2D.rectTransform.pivot = new Vector2(0, 1);
            _text.rectTransform.anchoredPosition = new Vector2(10, -40);
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
    }
}
