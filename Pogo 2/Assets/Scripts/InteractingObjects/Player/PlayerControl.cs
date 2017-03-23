using Enums.Input;
using UnityEngine;

namespace InteractingObjects.Player
{
    public class PlayerControl
    {
        public Vector3 MousePosition;

        public Vector2 GetMoveDirection(Rigidbody2D playerRigidbody2D)
        {
            float deltaX = -playerRigidbody2D.rotation;
            float deltaY = Mathf.Sin(Mathf.Deg2Rad * (playerRigidbody2D.rotation + 90)) * 50;
            var moveDirection = new Vector2(deltaX, deltaY);
            return moveDirection;
        }

        public Quaternion GetRotationAngleInput(Rigidbody2D playerRigidbody2D, InputDeviceEnum inputDeviceEnum)
        {
            var inputPositionInWorld = GetInputPositionInWorld(inputDeviceEnum);
            float deltaX = inputPositionInWorld.x - playerRigidbody2D.position.x;
            float angle = -deltaX;
            return Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

        public Vector3 GetInputPositionInWorld(InputDeviceEnum inputDeviceEnum)
        {
            var inputPosition = inputDeviceEnum == InputDeviceEnum.KeyboardAndMouse ? Input.mousePosition : (Vector3)Input.touches[0].position;
            return Camera.main.ScreenToWorldPoint(inputPosition);
        }
    }
}
