using Assets.Engine;
using UnityEngine;

namespace Assets.Scripts.GUI.Buttons
{
    public class ContinueButton : MonoBehaviour
    {
        public void OnClick()
        {
            GameEngineHelper.GetCurrentGameEngine().Pause();
        }
    }
}
