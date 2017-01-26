using Assets.Scripts.Interface.Controls.Sliders.Abstractions;
using UnityEngine;

namespace Assets.Scripts.Interface.Controls.Sliders
{
    public class HealthBarSlider : BaseNonInteractableSlider
    {
        protected override void Awake()
        {           
            base.Awake();
        }

        protected override void Start()
        {
            FillArea.ImageRenderer.color = Color.red;
            base.Start();
        }
    }
}
