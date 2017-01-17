using Assets.Scripts.Enums;
using Assets.Scripts.GUI;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class InputHandler : MonoBehaviour
    {
        private InputDeviceEnum _currentInputDevice;
        private InputEvents _inputEvents;
        private bool _ignorePlayerInputs;

        InputHandler()
        {
            _ignorePlayerInputs = false;
            _inputEvents = new InputEvents();
        }

        void Update()
        {
            ChangeInputDevice();
            if (_currentInputDevice == InputDeviceEnum.KeyboardAndMouse)
            {
                _inputEvents.CheckAllInputsForMouseEvents();
            }
            if (_currentInputDevice == InputDeviceEnum.TouchDevice)
            {
                _inputEvents.CheckAllInputsForTouchEvents();
            }
        }

        public void PlayerSubscribe(Player player)
        {
            _inputEvents.PrimaryActionInputTriggered += player.ProcessInputs;
            _inputEvents.MovementActionInputTriggered += player.ProcessInputs;
        }

        public void PlayerUnsubscribe(Player player)
        {
            _inputEvents.PrimaryActionInputTriggered -= player.ProcessInputs;
            _inputEvents.MovementActionInputTriggered -= player.ProcessInputs;
        }

        public void GuiSubscribe(GuiHandler guiHandler)
        {
            _inputEvents.PauseActionInputTriggered += guiHandler.ProcessInputs;
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
