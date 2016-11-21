using Assets.Scripts.Enums;
using Assets.Scripts.GUI;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Engine
{
    public class InputHandler : MonoBehaviour
    {
        private InputDeviceEnum _currentInputDevice;
        private InputEvents inputEvents;

        InputHandler()
        {
            inputEvents = new InputEvents();
        }

        void Update()
        {
            ChangeInputDevice();
            inputEvents.CheckAllInputsForEvents();
        }

        public void PlayerSubscribe(Player player)
        {
            inputEvents.LeftMouseButtonClicked += player.ProcessInputs;
        }

        public void GUISubscribe(GUIHandler guiHandler)
        {
            inputEvents.EscapeButtonClicked += guiHandler.ProcessInputs;
        }

        void ChangeInputDevice()
        {
            if (Input.touchPressureSupported)
            {
                _currentInputDevice = InputDeviceEnum.TouchDevice;
            }
        }
    }
}
