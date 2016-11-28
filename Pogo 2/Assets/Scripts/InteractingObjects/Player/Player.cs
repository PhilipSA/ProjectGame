using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _playerRigidbody2D;
        private PolygonCollider2D _playerFootCollider2D;

        private PlayerControl _playerControl;
        public PlayerBounceLogic PlayerBounceLogic;
        public PlayerHitpoints _playerHitpoints;

        public Collider2D BoxCollider2D { get; private set; }

        // Use this for initialization
        void Start ()
        {
            _playerControl = new PlayerControl();
            PlayerBounceLogic = new PlayerBounceLogic();
            _playerHitpoints = new PlayerHitpoints();

            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerRigidbody2D.freezeRotation = true;

            _playerFootCollider2D = GetComponentInChildren<PolygonCollider2D>();

            BoxCollider2D = GetComponent<BoxCollider2D>();
        }
	
        // Update is called once per frame
        void Update()
        {
            Bounce();
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
                AnglePlayer();
            }
        }

        public void OnHeadCollision()
        {
            _playerHitpoints.CalculateDamage();
            DeadCheck();
        }

        public void OnFootCollision()
        {
            AnglePlayer();
            MovePlayer();
        }

        void DeadCheck()
        {
            if (_playerHitpoints.Hitpoints <= 0)
            {
                GameEngineHelper.GetCurrentGameEngine().Defeat();
            }
        }

        void Bounce()
        {          
            _playerFootCollider2D.sharedMaterial.bounciness = PlayerBounceLogic.GetBouncePower();
        }

        void MovePlayer()
        {
            var moveDirection = _playerControl.GetMoveDirection(_playerRigidbody2D);
            _playerRigidbody2D.velocity = moveDirection;
        }

        void AnglePlayer()
        {
            var newRotationAngle = _playerControl.GetRotationAngle(_playerRigidbody2D);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotationAngle, 0.02f);
        }

        void StraightenUp()
        {
            var defaultAngle = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultAngle, 0.2f);
        }
    }
}
