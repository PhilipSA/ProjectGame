using Assets.Scripts.Enums;
using Assets.Scripts.InteractingObjects.Player;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts.Engine.Input
{
    public class InputHandler : MonoBehaviour
    {
        private InputDeviceEnum _currentInputDevice;
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
            if (_currentInputDevice == InputDeviceEnum.KeyboardAndMouse)
            {
                _inputEvents.CheckAllInputsForMouseEvents();
            }
            if (_currentInputDevice == InputDeviceEnum.TouchDevice)
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
                _currentInputDevice = InputDeviceEnum.TouchDevice;
            }
            if (UnityEngine.Input.mousePresent)
            {
                _currentInputDevice = InputDeviceEnum.KeyboardAndMouse;
            }
        }
    }
}
