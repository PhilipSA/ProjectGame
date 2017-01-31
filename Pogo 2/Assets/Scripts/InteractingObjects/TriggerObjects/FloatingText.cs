using Assets.Scripts.CustomComponents;
using Assets.Scripts.Engine;
using Assets.Scripts.Interface;
using SmartLocalization;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.TriggerObjects
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
            Debug.Log(LanguageManager.Instance.GetTextValue(DisplayText));
            DisplayText = LanguageManager.Instance.GetTextValue(DisplayText);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("TEST");
            GameEngineHelper.GetCurrentGameEngine().InterfaceHandler.DisplayFloatingText(DisplayText);
            enabled = false;
        }
    }
}
