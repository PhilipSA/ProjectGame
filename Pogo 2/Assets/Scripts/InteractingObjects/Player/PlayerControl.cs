using System;
using Enums.Input;
using InteractingObjects.Player.Parts;
using UnityEngine;

namespace InteractingObjects.Player
{
    public class PlayerControl
    {
        public Vector3 MousePosition;
        public Player Player;
        private int minBounceStep = -5;
        private int maxBounceStep = 5;

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
            float deltaX = inputPositionInWorld.x < playerRigidbody2D.position.x ? 
                Math.Abs(inputPositionInWorld.x - playerRigidbody2D.position.x) :
                -Math.Abs(inputPositionInWorld.x - playerRigidbody2D.position.x);
            return Quaternion.Euler(new Vector3(0f, 0f, Mathf.Clamp(deltaX, -75, 75)));
        }

        public Vector3 GetInputPositionInWorld(InputDeviceEnum inputDeviceEnum)
        {
            return Camera.main.ScreenToWorldPoint(GetPositionOfInput(inputDeviceEnum));
        }

        public void MovePlayerOnBounce(InputDeviceEnum inputDeviceEnum)
        {
            AnglePlayerTowardsInputOnBounce(inputDeviceEnum);
            var moveDirection = GetMoveDirection(Player.PlayerRigidbody2D);
            Player.PlayerRigidbody2D.AddForce(new Vector2(moveDirection.x*1.5f*Player.PlayerBounceLogic.GetRandomBouncePower(), 100+moveDirection.y*2*Player.PlayerBounceLogic.GetRandomBouncePower()), ForceMode2D.Impulse);
        }

        public void AnglePlayerTowardsInputOnBounce(InputDeviceEnum inputDeviceEnum)
        {
            var inputPosition = GetPositionOfInput(inputDeviceEnum);
            var normalizedAngle = inputPosition.x < Camera.main.WorldToScreenPoint(Player.transform.position).x
                    ? -Mathf.Abs(inputPosition.x - Camera.main.WorldToScreenPoint(Player.transform.position).x)
                    : Mathf.Abs(inputPosition.x - Camera.main.WorldToScreenPoint(Player.transform.position).x);
            var factor = Mathf.Clamp(normalizedAngle/100, minBounceStep, maxBounceStep);
            Player.PlayerRigidbody2D.AddTorque(-factor*500, ForceMode2D.Impulse);
        }

        public void AnglePlayerTowardsInputOnChange(InputDeviceEnum inputDeviceEnum, float rotationFactor)
        {
            var newRotationAngle = GetRotationAngleInput(Player.PlayerRigidbody2D, inputDeviceEnum);
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, newRotationAngle, rotationFactor);
            Player.PlayerRigidbody2D.AddForce(new Vector2(Player.PlayerRigidbody2D.rotation / 100, 0));
        }

        public void StraightenUp()
        {
            Player.PlayerRigidbody2D.AddTorque(-50);
        }

        public Vector3 GetPositionOfInput(InputDeviceEnum inputDeviceEnum)
        {
            return inputDeviceEnum == InputDeviceEnum.KeyboardAndMouse ? Input.mousePosition : (Vector3)Input.touches[0].position;
        }
    }
}
