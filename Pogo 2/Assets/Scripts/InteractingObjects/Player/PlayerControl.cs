using Enums.Input;
using UnityEngine;

namespace InteractingObjects.Player
{
    public class PlayerControl
    {
        public Vector3 MousePosition;
        public Player Player;

        public PlayerControl(Player player)
        {
            Player = player;
        }

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

        public void MovePlayerOnBounce()
        {
            var moveDirection = GetMoveDirection(Player.PlayerRigidbody2D);
            moveDirection.y *= Player.PlayerBounceLogic.GetRandomBouncePower();
            moveDirection.x *= Player.PlayerBounceLogic.GetRandomBouncePower();
            Player.PlayerRigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }

        public void AnglePlayerTowardsInput(InputDeviceEnum inputDeviceEnum, float rotationFactor)
        {
            var newRotationAngle = GetRotationAngleInput(Player.PlayerRigidbody2D, inputDeviceEnum);
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, newRotationAngle, rotationFactor);
            Player.PlayerRigidbody2D.velocity = new Vector2(Player.PlayerRigidbody2D.velocity.x - Player.PlayerRigidbody2D.rotation / 100, Player.PlayerRigidbody2D.velocity.y);
        }

        public void StraightenUp()
        {
            var defaultAngle = Quaternion.Euler(0, 0, 0);
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, defaultAngle, 0.099f);
        }
    }
}
