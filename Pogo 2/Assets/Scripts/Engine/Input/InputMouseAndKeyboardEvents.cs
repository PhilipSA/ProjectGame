using Enums.Input;
using UnityEngine;

namespace Engine.Input
{
    public class InputMouseAndKeyboardEvents
    {
        public InputActionEnum? CheckForInputs()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                return InputActionEnum.PrimaryAction;
            }
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                return InputActionEnum.SecondaryAction;
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
