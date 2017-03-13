using Assets.Scripts.Engine;
using Assets.Scripts.InteractingObjects.Player.Parts;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _playerRigidbody2D;

        private PlayerFoot _playerFoot;
        private PlayerHead _playerHead;

        private PlayerControl _playerControl;
        public PlayerBounceLogic PlayerBounceLogic;
        public PlayerHitpoints PlayerHitpoints;

        public Collider2D BoxCollider2D { get; private set; }

        void Awake()
        {
            _playerFoot = GetComponentInChildren<PlayerFoot>();
            _playerHead = GetComponentInChildren<PlayerHead>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start ()
        {
            _playerControl = new PlayerControl();
            PlayerBounceLogic = new PlayerBounceLogic();
            PlayerHitpoints = new PlayerHitpoints();

            _playerRigidbody2D.freezeRotation = true;

            BoxCollider2D = GetComponent<BoxCollider2D>();
        }
	
        // Update is called once per frame
        void Update()
        {
            PlayerBounceLogic.UpdateBouncePower();
            StraightenUp();
        }

        public void PrimaryActionInvoke()
        {
            PlayerBounceLogic.IncreaseBouncePower();
        }

        public void MovementInvoke()
        {
            AnglePlayerTowardsMouse();
        }

        public void OnTrampolineCollision()
        {
            _playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, _playerRigidbody2D.velocity.y + 200);
        }

        public void OnHeadCollision()
        {
            PlayerHitpoints.CalculateImpactDamage(_playerRigidbody2D);
            DeadCheck();
        }

        public void OnHazardCollision()
        {
            PlayerHitpoints.InflictHazardDamage();
            _playerHead.AnimateDamage();
            DeadCheck();
        }

        public void OnFootCollision()
        {
            AnglePlayer();
            MovePlayer();
        }

        void DeadCheck()
        {
            if (PlayerHitpoints.Hitpoints <= 0 && enabled)
            {
                GameEngineHelper.GetCurrentGameEngine().Defeat();
                _playerHead.AnimateDeath();
            }
        }

        void MovePlayer()
        {
            var moveDirection = _playerControl.GetMoveDirection(_playerRigidbody2D);
            moveDirection.y *= PlayerBounceLogic.GetBouncePower();
            moveDirection.x *= PlayerBounceLogic.GetBouncePower();
            _playerRigidbody2D.velocity = new Vector2(moveDirection.x + _playerRigidbody2D.velocity.x, moveDirection.y + _playerRigidbody2D.velocity.y);
        }
        
        void AnglePlayer()
        {
            var newRotationAngle = _playerControl.GetRotationAngle(_playerRigidbody2D);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotationAngle, 0.1f);
        }

        void AnglePlayerTowardsMouse()
        {
            var newRotationAngle = _playerControl.GetRotationAngle(_playerRigidbody2D);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotationAngle, 0.5f);
        }

        void StraightenUp()
        {
            var defaultAngle = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultAngle, 0.099f);
        }
    }
}
