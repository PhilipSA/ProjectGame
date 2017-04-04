using System;
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
        public Vector3 LastInputPosition;

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
            _inputEvents.SecondaryActionInputTriggered += player.SecondaryActionInvoke;
            _inputEvents.MovementActionInputTriggered += player.MovementInvoke;
        }

        public void PlayerUnsubscribeToInputs(Player player)
        {
            _inputEvents.PrimaryActionInputTriggered -= player.PrimaryActionInvoke;
            _inputEvents.SecondaryActionInputTriggered -= player.SecondaryActionInvoke;
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

        public Vector3 GetLastPositionOfInput()
        {
            if (CurrentInputDevice == InputDeviceEnum.KeyboardAndMouse) return UnityEngine.Input.mousePosition;
            if (CurrentInputDevice == InputDeviceEnum.TouchDevice)
            {
                LastInputPosition = Math.Abs(UnityEngine.Input.touches[0].position.x) < 0 ? LastInputPosition : (Vector3)UnityEngine.Input.touches[0].position;
                return LastInputPosition;
            }
            return new Vector3();
        }
    }
}
