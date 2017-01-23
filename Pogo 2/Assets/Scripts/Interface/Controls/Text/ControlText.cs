
using UnityEngine;

namespace Assets.Scripts.Interface.Controls.Text
{
    public class ControlText : UnityEngine.UI.Text
    {
        protected override void Start()
        {
            color = Color.gray;
            font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            base.Start();
        }
    }
}
