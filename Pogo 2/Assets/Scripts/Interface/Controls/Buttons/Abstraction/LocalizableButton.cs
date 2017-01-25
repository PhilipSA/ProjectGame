using Assets.Scripts.Engine.Audio;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Interface.Controls.Abstraction;
using Assets.Scripts.Interface.Controls.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Buttons.Abstraction
{
    public abstract class LocalizableButton : Button, ILocalizableControl
    {
        public string DisplayText { get; set; }
        public ControlText ButtonText { get; set; }
        public AudioSource AudioSource { get; set; }
        public Image ButtonImage { get; set; }

        public virtual void OnClick()
        {
            if (AudioSource != null)
            {
                AudioHandler.PlayAudio(AudioSource);
            }
        }

        protected override void Start()
        {
            onClick.AddListener(OnClick);

            ButtonText = CreateGameObject.CreateChildGameObject<ControlText>(transform).GetComponent<ControlText>();
            ButtonText.alignment = TextAnchor.MiddleCenter;

            GetComponentInChildren<ControlText>().text = DisplayText;

            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.clip = Resources.Load("Audio/InterfaceAudio/FREESOUND_buttonPop") as AudioClip;

            gameObject.AddComponent<CanvasRenderer>();

            ButtonImage = gameObject.AddComponent<Image>();
            ButtonImage.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            ButtonImage.type = Image.Type.Sliced;
            targetGraphic = ButtonImage;
        }
    }
}
