using CustomComponents;
using Engine;
using Engine.Input;
using Enums.Input;
using UnityEngine;

namespace GameObjects.InteractingObjects.Player
{
    public class PlayerControl
    {
        public Vector3 MousePosition;
        public Player Player;
        private const int MinBounceStep = -5;
        private const int MaxBounceStep = 5;
        private const float MinRotation = -75;
        private const float MaxRotation = 75;
        private const float MaxNegativeInputDistance = -1000;
        private const float MaxPositiveInputDistance = 1000;

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
            float deltaX = inputPositionInWorld.x - playerRigidbody2D.position.x;
            var forceFactor = IntervalConverter.ConvertValueInIntervalToOtherIntervalValue(MaxNegativeInputDistance, MaxPositiveInputDistance, MinRotation,
                MaxRotation, deltaX);
            return Quaternion.Euler(new Vector3(0f, 0f, forceFactor));
        }

        public Vector3 GetLastInputPositionInWorld()
        {
            return Camera.main.ScreenToWorldPoint(GameEngineHelper.GetCurrentGameEngine().InputHandler.GetLastPositionOfInput());
        }

        public void MovePlayerOnBounce()
        {
            if (InputHandler.CurrentInputDevice == InputDeviceEnum.KeyboardAndMouse) AnglePlayerTowardsInputOnBounce();
            var moveDirection = GetMoveDirection(Player.PlayerRigidbody2D);
            Player.PlayerRigidbody2D.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
            Player.PlayerRigidbody2D.AddRelativeForce(moveDirection*Player.PlayerBounceLogic.GetRandomBouncePower(), ForceMode2D.Impulse);
        }

        public void AnglePlayerTowardsInputOnBounce()
        {
            var inputPosition = GameEngineHelper.GetCurrentGameEngine().InputHandler.GetLastPositionOfInput();
            var normalizedAngle = inputPosition.x - Camera.main.WorldToScreenPoint(Player.transform.position).x;
            var forceFactor = IntervalConverter.ConvertValueInIntervalToOtherIntervalValue(MaxNegativeInputDistance, MaxPositiveInputDistance, MinBounceStep,
                MaxBounceStep, normalizedAngle);
            Player.PlayerRigidbody2D.AddTorque(-forceFactor*100, ForceMode2D.Impulse);
        }

        public void AnglePlayerTowardsInputOnChange(float rotationFactor)
        {
            var newRotationAngle = GetRotationAngleInput(Player.PlayerRigidbody2D);
            Player.PlayerRigidbody2D.AddTorque(rotationFactor*newRotationAngle.z);
            Player.PlayerRigidbody2D.AddForce(new Vector2(Player.PlayerRigidbody2D.rotation / 100, 0));
        }

        public void StraightenUp()
        {
            Player.PlayerRigidbody2D.AddTorque(-Player.PlayerRigidbody2D.rotation*15);
        }
    }
}
