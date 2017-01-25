using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Controls.Text
{
    public class ControlText : UnityEngine.UI.Text
    {
        protected override void OnEnable()
        {
            color = Color.black;
            font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            fontSize = 14;
        }
    }
}
