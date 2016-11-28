using Assets.Scripts.Enums;
using Assets.Scripts.GUI;
using Assets.Scripts.GUI.GUIElements;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;

namespace Assets.Engine
{
    public class InputHandler : MonoBehaviour
    {
        private InputDeviceEnum _currentInputDevice;
        private InputEvents inputEvents;
        private bool _ignorePlayerInputs;

        InputHandler()
        {
            _ignorePlayerInputs = false;
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
            inputEvents.MouseMovementDetected += player.ProcessInputs;
        }

        public void PlayerUnsubscribe(Player player)
        {
            inputEvents.LeftMouseButtonClicked -= player.ProcessInputs;
            inputEvents.MouseMovementDetected -= player.ProcessInputs;
        }

        public void GUISubscribe(GUIHandler guiHandler)
        {
            inputEvents.EscapeButtonClicked += guiHandler.ProcessInputs;
        }

        public void ToggleIgnorePlayerInputs(bool ignoreInputs, Player player)
        {
            _ignorePlayerInputs = ignoreInputs;
            if (_ignorePlayerInputs) PlayerUnsubscribe(player); else PlayerSubscribe(player);
        }

        void ChangeInputDevice()
        {
            if (Input.touchPressureSupported)
            {
                _currentInputDevice = InputDeviceEnum.TouchDevice;
            }
            if (Input.mousePresent)
            {
                _currentInputDevice = InputDeviceEnum.KeyboardAndMouse;
            }
        }
    }
}
