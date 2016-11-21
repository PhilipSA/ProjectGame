using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Engine
{
    public class InputHandler : MonoBehaviour
    {
        private InputDeviceEnum _currentInputDevice;
        private event InputCaller handleInput;
        private delegate void InputCaller(string e); 

        void Update()
        {
            ChangeInputDevice();
            CheckAllInputsForEvents();
        }

        public void PlayerSubscribe(Player player)
        {
            handleInput += player.ProcessInputs;
        }

        private void CheckAllInputsForEvents()
        {
            if (Input.GetMouseButtonDown(0))
            {
                handleInput.Invoke("test");
            }
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
