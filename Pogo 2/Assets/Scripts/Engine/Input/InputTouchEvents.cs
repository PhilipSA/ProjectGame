using Assets.Scripts.Enums.Input;
using UnityEngine;

namespace Assets.Scripts.Engine.Input
{
    public class InputTouchEvents
    {
        public InputActionEnum? CheckForInputs()
        {
            var touch = UnityEngine.Input.GetTouch(0);
            var start = 0f;

            if (touch.phase == TouchPhase.Began) start = touch.position.magnitude;

            if (touch.phase == TouchPhase.Ended && touch.tapCount == 1 && touch.deltaPosition.magnitude<start)
            {
                return InputActionEnum.PrimaryAction;
            }
            if (touch.fingerId == 2)
            {
                return InputActionEnum.PauseAction;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                return InputActionEnum.MovementAction;
            }

            return null;
        }
    }
}
