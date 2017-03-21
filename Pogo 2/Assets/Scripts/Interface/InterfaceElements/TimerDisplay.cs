using CustomComponents;
using GameObjects;
using GameObjects.Components.Controls.Text;
using GameObjects.Components.Image;
using Interface.InterfaceElements.Abstraction;
using UnityEngine;

namespace Interface.InterfaceElements
{
    public class TimerDisplay : InterfaceElement
    {
        public StopWatch StopWatch;
        public ControlText Text { get; private set; }
        public CustomImage Texture2D { get; private set; }

        // Use this for initialization
        protected override void Awake()
        {
            Texture2D = CreateGameObject.CreateChildGameObject<CustomImage>(transform).GetComponent<CustomImage>();      
            Text = CreateGameObject.CreateChildGameObject<ControlText>(Texture2D.rectTransform).GetComponent<ControlText>();
            StopWatch = gameObject.AddComponent<StopWatch>();
            base.Awake();
        }

        void Start()
        {
            Text.alignment = TextAnchor.MiddleCenter;
            Text.color = Color.white;
            Text.fontSize = 25;

            Texture2D.color = new Color32(67, 67, 67, 255);
            Texture2D.SetAnchorsAndPivot(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1));
            Texture2D.rectTransform.sizeDelta = new Vector2(120, 30);

            StopWatch.StartTimer();
        }

        // Update is called once per frame
        void Update()
        {
            Text.text = StopWatch.GetTimeInMmssffFormat();
        }

        public void StopTimer()
        {
            StopWatch.StopTimer();
        }
    }
}
