using Engine;
using Enums.Input;
using InteractingObjects.Player.Parts;
using UnityEngine;

namespace InteractingObjects.Player
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D PlayerRigidbody2D { get; private set; }

        public PlayerFoot PlayerFoot;
        public PlayerHead PlayerHead;

        private PlayerControl _playerControl;
        public PlayerBounceLogic PlayerBounceLogic;
        public PlayerHitpoints PlayerHitpoints;
        public PlayerCollider PlayerCollider;

        public Collider2D BoxCollider2D { get; private set; }

        void Awake()
        {
            PlayerCollider = new PlayerCollider(this);
            PlayerFoot = GetComponentInChildren<PlayerFoot>();
            PlayerHead = GetComponentInChildren<PlayerHead>();
            PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Start ()
        {
            _playerControl = new PlayerControl();
            PlayerBounceLogic = new PlayerBounceLogic();
            PlayerHitpoints = new PlayerHitpoints(this);

            BoxCollider2D = GetComponent<BoxCollider2D>();
        }
	
        // Update is called once per frame
        void Update()
        {
            PlayerBounceLogic.UpdateBouncePower();
            StraightenUp();
        }

        public void PrimaryActionInvoke(InputDeviceEnum inputDeviceEnum)
        {
            PlayerBounceLogic.IncreaseBouncePower();
        }

        public void SecondaryActionInvoke(InputDeviceEnum inputDeviceEnum)
        {
            PlayerBounceLogic.DecreaseBouncePower();
        }

        public void MovementInvoke(InputDeviceEnum inputDeviceEnum)
        {
            if (inputDeviceEnum == InputDeviceEnum.KeyboardAndMouse) AnglePlayerTowardsInput(inputDeviceEnum, 0.5f);
            if (inputDeviceEnum == InputDeviceEnum.TouchDevice) AnglePlayerTowardsInput(inputDeviceEnum, 1.5f);
        }

        public void DeadCheck()
        {
            if (PlayerHitpoints.Hitpoints <= 0 && enabled)
            {
                GameEngineHelper.GetCurrentGameEngine().Defeat();
                PlayerHead.AnimateDeath();
            }
        }

        public void MovePlayerOnBounce()
        {
            var moveDirection = _playerControl.GetMoveDirection(PlayerRigidbody2D);
            moveDirection.y *= PlayerBounceLogic.GetRandomBouncePower();
            moveDirection.x *= PlayerBounceLogic.GetRandomBouncePower();
            PlayerRigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }

        void AnglePlayerTowardsInput(InputDeviceEnum inputDeviceEnum, float rotationFactor)
        {
            var newRotationAngle = _playerControl.GetRotationAngleInput(PlayerRigidbody2D, inputDeviceEnum);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotationAngle, rotationFactor);
            PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x - PlayerRigidbody2D.rotation/100, PlayerRigidbody2D.velocity.y);
        }

        void StraightenUp()
        {
            var defaultAngle = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultAngle, 0.099f);
        }
    }
}
