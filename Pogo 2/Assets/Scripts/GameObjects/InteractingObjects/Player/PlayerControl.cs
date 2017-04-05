using System;
using Engine;
using Engine.Input;
using Enums.Input;
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
            return Vector2.up;
        }

        public Quaternion GetRotationAngleInput(Rigidbody2D playerRigidbody2D)
        {
            var inputPositionInWorld = GetLastInputPositionInWorld();
            float deltaX = inputPositionInWorld.x < playerRigidbody2D.position.x ? 
                Math.Abs(inputPositionInWorld.x - playerRigidbody2D.position.x) :
                -Math.Abs(inputPositionInWorld.x - playerRigidbody2D.position.x);
            return Quaternion.Euler(new Vector3(0f, 0f, Mathf.Clamp(deltaX, -75, 75)));
        }

        public Vector3 GetLastInputPositionInWorld()
        {
            return Camera.main.ScreenToWorldPoint(GameEngineHelper.GetCurrentGameEngine().InputHandler.GetLastPositionOfInput());
        }

        public void MovePlayerOnBounce()
        {
            if (InputHandler.CurrentInputDevice == InputDeviceEnum.KeyboardAndMouse) AnglePlayerTowardsInputOnBounce();
            var moveDirection = GetMoveDirection(Player.PlayerRigidbody2D);
            Player.PlayerRigidbody2D.AddForce(new Vector2(0, 100), ForceMode2D.Impulse);
            Player.PlayerRigidbody2D.AddRelativeForce(moveDirection*Player.PlayerBounceLogic.GetRandomBouncePower(), ForceMode2D.Impulse);
        }

        public void AnglePlayerTowardsInputOnBounce()
        {
            var inputPosition = GameEngineHelper.GetCurrentGameEngine().InputHandler.GetLastPositionOfInput();
            var normalizedAngle = inputPosition.x < Camera.main.WorldToScreenPoint(Player.transform.position).x
                    ? -Mathf.Abs(inputPosition.x - Camera.main.WorldToScreenPoint(Player.transform.position).x)
                    : Mathf.Abs(inputPosition.x - Camera.main.WorldToScreenPoint(Player.transform.position).x);
            var factor = Mathf.Clamp(normalizedAngle/100, minBounceStep, maxBounceStep);
            Player.PlayerRigidbody2D.AddTorque(-factor*50, ForceMode2D.Impulse);
        }

        public void AnglePlayerTowardsInputOnChange(float rotationFactor)
        {
            var newRotationAngle = GetRotationAngleInput(Player.PlayerRigidbody2D);
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, newRotationAngle, rotationFactor);
            Player.PlayerRigidbody2D.AddForce(new Vector2(Player.PlayerRigidbody2D.rotation / 100, 0));
        }

        public void StraightenUp()
        {
            Player.PlayerRigidbody2D.AddTorque(-Player.PlayerRigidbody2D.rotation*5);
        }
    }
}
