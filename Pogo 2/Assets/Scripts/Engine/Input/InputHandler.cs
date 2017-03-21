using Enums.Input;
using InteractingObjects.Player;
using Interface;
using UnityEngine;

namespace Engine.Input
{
    public class InputHandler : MonoBehaviour
    {
        public static InputDeviceEnum CurrentInputDevice;
        private InputEvents _inputEvents;
        private bool _ignorePlayerInputs;

        void Awake()
        {
            _ignorePlayerInputs = false;
            _inputEvents = new InputEvents();
        }

        void Update()
        {
            ChangeInputDevice();
            if (CurrentInputDevice == InputDeviceEnum.KeyboardAndMouse)
            {
                _inputEvents.CheckAllInputsForMouseEvents();
            }
            if (CurrentInputDevice == InputDeviceEnum.TouchDevice)
            {
                _inputEvents.CheckAllInputsForTouchEvents();
            }
        }

        public void PlayerSubscribeToInputs(Player player)
        {
            _inputEvents.PrimaryActionInputTriggered += player.PrimaryActionInvoke;
            _inputEvents.MovementActionInputTriggered += player.MovementInvoke;
        }

        public void PlayerUnsubscribeToInputs(Player player)
        {
            _inputEvents.PrimaryActionInputTriggered -= player.PrimaryActionInvoke;
            _inputEvents.MovementActionInputTriggered -= player.MovementInvoke;
        }

        public void GuiSubscribe(InterfaceHandler interfaceHandler)
        {
            _inputEvents.PauseActionInputTriggered += interfaceHandler.PauseInvoked;
        }

        public void ToggleIgnorePlayerInputs(bool ignoreInputs, Player player)
        {
            _ignorePlayerInputs = ignoreInputs;
            if (_ignorePlayerInputs) PlayerUnsubscribeToInputs(player); else PlayerSubscribeToInputs(player);
        }

        void ChangeInputDevice()
        {
            if (UnityEngine.Input.touchSupported)
            {
                CurrentInputDevice = InputDeviceEnum.TouchDevice;
            }
            if (UnityEngine.Input.mousePresent)
            {
                CurrentInputDevice = InputDeviceEnum.KeyboardAndMouse;
            }
        }
    }
}
