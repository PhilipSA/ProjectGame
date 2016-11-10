using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerControl
    {
        public Vector3 MousePosition;

        public Vector2 GetMoveDirection(Rigidbody2D playerRigidbody2D)
        {
            float deltaX = -playerRigidbody2D.rotation;
            var moveDirection = new Vector2(deltaX, playerRigidbody2D.velocity.y);
            return moveDirection;
        }

        public Quaternion GetRotationAngle(Rigidbody2D playerRigidbody2D)
        {
            var mousePositionInWorld = GetMousePositionInWorld();
            float deltaX = mousePositionInWorld.x - playerRigidbody2D.position.x;
            float angle = -deltaX;
            return Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

        public Vector3 GetMousePositionInWorld()
        {
            var mousePosition = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }

        public bool HasMousePositionChanged()
        {
            if (Input.mousePosition != MousePosition)
            {
                MousePosition = Input.mousePosition;
                return true;
            }
            return false;
        }
    }
}
