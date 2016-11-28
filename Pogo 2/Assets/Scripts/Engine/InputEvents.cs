using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class InputEvents
    {
        public delegate void InputProcess(KeyCode keyCode);

        public event InputProcess LeftMouseButtonClicked;
        public event InputProcess EscapeButtonClicked;
        public event InputProcess MouseMovementDetected;

        public void CheckAllInputsForEvents()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (LeftMouseButtonClicked != null) LeftMouseButtonClicked.Invoke(KeyCode.Mouse0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (EscapeButtonClicked != null) EscapeButtonClicked.Invoke(KeyCode.Escape);
            }
            if (Input.GetAxis("Mouse X") != 0 || (Input.GetAxis("Mouse Y") != 0))
            {
                if (MouseMovementDetected != null) MouseMovementDetected.Invoke(KeyCode.Mouse6);
            }
        }
    }
}
