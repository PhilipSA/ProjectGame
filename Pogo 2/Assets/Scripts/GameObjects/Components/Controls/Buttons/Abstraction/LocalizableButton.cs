using Assets.Scripts.Engine.Audio;
using Assets.Scripts.GameObjects.Components.Controls.Abstraction;
using Assets.Scripts.GameObjects.Components.Controls.Text;
using Assets.Scripts.GameObjects.Components.Image;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Components.Controls.Buttons.Abstraction
{
    public abstract class LocalizableButton : Button, ILocalizableControl
    {
        public string DisplayText { get; set; }
        public ControlText ButtonText { get; set; }
        public AudioSource AudioSource { get; private set; }
        public CustomImage ButtonImage { get; private set; }
        public RectTransform RectTransform { get; private set; }

        public virtual void OnClick()
        {
            if (AudioSource != null)
            {
                AudioHandler.PlayAudio(AudioSource);
            }
        }

        protected override void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            ButtonText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            gameObject.AddComponent<CanvasRenderer>();
            ButtonImage = gameObject.AddComponent<CustomImage>();
            ButtonImage.Initialize(Resources.Load<Sprite>("UI/Skin/background"), UnityEngine.UI.Image.Type.Sliced);
            RectTransform = GetComponent<RectTransform>();
        }

        protected override void Start()
        {
            onClick.AddListener(OnClick);
            ButtonText.alignment = TextAnchor.MiddleCenter;
            GetComponentInChildren<ControlText>().text = DisplayText;
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InterfaceAudio/FREESOUND_buttonPop");
            targetGraphic = ButtonImage;
        }

        public void SetAnchors(Vector2 anchorMin, Vector2 anchorMax)
        {
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
        }

        public void SetAnchorsAndPivot(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
            RectTransform.pivot = pivot;
        }
    }
}
