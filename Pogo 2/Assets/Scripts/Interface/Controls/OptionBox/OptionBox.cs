using Assets.Scripts.Interface.Controls.Abstraction;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interface.Controls.OptionBox
{
    public abstract class OptionBox : UIBehaviour, ILocalizableControl
    {
        public string DisplayText { get; set; }
        protected bool ValueChanged;

        protected virtual void OnValueChanged(float newValue)
        {
            ValueChanged = true;
        }

    }
}
