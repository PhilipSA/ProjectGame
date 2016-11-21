using UnityEngine;

namespace Assets.Engine
{
    public class InputEvents
    {
        public delegate void InputProcess(KeyCode keyCode);

        public event InputProcess LeftMouseButtonClicked;
        public event InputProcess EscapeButtonClicked;

        public void CheckAllInputsForEvents()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonClicked.Invoke(KeyCode.Mouse0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeButtonClicked.Invoke(KeyCode.Escape);
            }
        }
    }
}
