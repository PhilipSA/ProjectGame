using Assets.Scripts.Enums.Input;
using UnityEngine;

namespace Assets.Scripts.Engine.Input
{
    public class InputTouchEvents
    {
        private Vector2 _startPosition;

        public InputActionEnum? CheckForInputs()
        {
            var touch = UnityEngine.Input.GetTouch(0);          

            if (touch.phase == TouchPhase.Began) _startPosition = touch.position;

            if (touch.phase == TouchPhase.Ended && touch.position == _startPosition)
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
