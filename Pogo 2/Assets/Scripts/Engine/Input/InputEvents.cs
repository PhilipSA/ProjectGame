using Enums.Input;

namespace Engine.Input
{
    public class InputEvents
    {
        public delegate void InputProcess(InputDeviceEnum inputDeviceEnum);

        public event InputProcess PrimaryActionInputTriggered;
        public event InputProcess PauseActionInputTriggered;
        public event InputProcess MovementActionInputTriggered;

        public InputTouchEvents InputTouchEvents;
        public InputMouseAndKeyboardEvents InputMouseAndKeyboardEvents;

        public InputEvents()
        {
            InputTouchEvents = new InputTouchEvents();
            InputMouseAndKeyboardEvents = new InputMouseAndKeyboardEvents();
        }

        public void CheckAllInputsForMouseEvents()
        {
            var actionInput = InputMouseAndKeyboardEvents.CheckForInputs();
            if (actionInput != null) InvokeEventFromInputAction(actionInput.Value);
        }

        public void CheckAllInputsForTouchEvents()
        {
            var actionInput = InputTouchEvents.CheckForInputs();
            if (actionInput != null) InvokeEventFromInputAction(actionInput.Value);
        }

        public void InvokeEventFromInputAction(InputActionEnum inputActionEnum)
        {
            if (inputActionEnum == InputActionEnum.PrimaryAction)
                if (PrimaryActionInputTriggered != null) PrimaryActionInputTriggered.Invoke(InputHandler.CurrentInputDevice);
            if (inputActionEnum == InputActionEnum.PauseAction)
                if (PauseActionInputTriggered != null) PauseActionInputTriggered.Invoke(InputHandler.CurrentInputDevice);
            if (inputActionEnum == InputActionEnum.MovementAction)
                if (MovementActionInputTriggered != null) MovementActionInputTriggered.Invoke(InputHandler.CurrentInputDevice);
        }
    }
}
