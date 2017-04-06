using Engine;
using SmartLocalization;
using UnityEngine;

namespace GameObjects.InteractingObjects.TriggerObjects
{
    public class FloatingText : TriggerObject
    {
        public string DisplayText;

        public override void Awake()
        {
            base.Awake();
        }

        public void Start()
        {
            DisplayText = LanguageManager.Instance.GetTextValue(DisplayText);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            GameEngineHelper.GetCurrentGameEngine().InterfaceHandler.DisplayFloatingText(DisplayText);
            enabled = false;
        }
    }
}
