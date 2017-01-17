using Assets.Scripts.Engine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface.Buttons
{
    public class ContinueButton : Button
    {
        public void OnClick()
        {
            GameEngineHelper.GetCurrentGameEngine().TogglePause();
        }
    }
}
