using Assets.Scripts.Enums.Input;
using UnityEngine;

namespace Assets.Scripts.Engine.Input
{
    public class InputMouseAndKeyboardEvents
    {
        public InputActionEnum? CheckForInputs()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                return InputActionEnum.PrimaryAction;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                return InputActionEnum.PauseAction;
            }
            if (UnityEngine.Input.GetAxis("Mouse X") != 0 || (UnityEngine.Input.GetAxis("Mouse Y") != 0))
            {
                return InputActionEnum.MovementAction;
            }
            return null;
        }
    }
}
