using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class InputEvents
    {
        public delegate void InputProcess(KeyCode keyCode);

        public event InputProcess PrimaryActionInputTriggered;
        public event InputProcess PauseActionInputTriggered;
        public event InputProcess MovementActionInputTriggered;

        public void CheckAllInputsForMouseEvents()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (PrimaryActionInputTriggered != null) PrimaryActionInputTriggered.Invoke(KeyCode.Mouse0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseActionInputTriggered != null) PauseActionInputTriggered.Invoke(KeyCode.Escape);
            }
            if (Input.GetAxis("Mouse X") != 0 || (Input.GetAxis("Mouse Y") != 0))
            {
                if (MovementActionInputTriggered != null) MovementActionInputTriggered.Invoke(KeyCode.Mouse6);
            }
        }

        public void CheckAllInputsForTouchEvents()
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (PrimaryActionInputTriggered != null) PrimaryActionInputTriggered.Invoke(KeyCode.Mouse0);
            }
            if (Input.GetTouch(0).fingerId == 2)
            {
                if (PauseActionInputTriggered != null) PauseActionInputTriggered.Invoke(KeyCode.Escape);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (MovementActionInputTriggered != null) MovementActionInputTriggered.Invoke(KeyCode.Mouse6);
            }
        }
    }
}
