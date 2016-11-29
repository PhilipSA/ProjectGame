using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerControl
    {
        public Vector3 MousePosition;

        public Vector2 GetMoveDirection(Rigidbody2D playerRigidbody2D)
        {
            float deltaX = -playerRigidbody2D.rotation;
            float deltaY = Mathf.Sin(Mathf.Deg2Rad * (playerRigidbody2D.rotation + 90)) * 50;
            Debug.Log(deltaY);
            var moveDirection = new Vector2(deltaX, deltaY);
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
    }
}
