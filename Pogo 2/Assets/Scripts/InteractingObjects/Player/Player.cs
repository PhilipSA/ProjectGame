using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _playerRigidbody2D;
        private CircleCollider2D _playerFootCollider2D;

        private PlayerControl _playerControl;
        public PlayerBounceLogic PlayerBounceLogic;
        public PlayerHitpoints PlayerHitpoints;

        public Collider2D BoxCollider2D { get; private set; }

        // Use this for initialization
        void Start ()
        {
            _playerControl = new PlayerControl();
            PlayerBounceLogic = new PlayerBounceLogic();
            PlayerHitpoints = new PlayerHitpoints();

            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerRigidbody2D.freezeRotation = true;

            _playerFootCollider2D = GetComponentInChildren<CircleCollider2D>();

            BoxCollider2D = GetComponent<BoxCollider2D>();
        }
	
        // Update is called once per frame
        void Update()
        {
            PlayerBounceLogic.UpdateBouncePower();
            StraightenUp();
        }

        public void ProcessInputs(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Mouse0)
            {
                PlayerBounceLogic.IncreaseBouncePower();
            }
            if (keyCode == KeyCode.Mouse6)
            {
                AnglePlayerTowardsMouse();
            }
        }

        public void OnTrampolineCollision()
        {
            Debug.Log("TEST");
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
            DeadCheck();
        }

        public void OnFootCollision()
        {
            AnglePlayer();
            MovePlayer();
        }

        void DeadCheck()
        {
            if (PlayerHitpoints.Hitpoints <= 0)
            {
                GameEngineHelper.GetCurrentGameEngine().Defeat();
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
