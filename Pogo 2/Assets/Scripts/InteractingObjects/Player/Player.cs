using Assets.Engine;
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
        public PlayerHitpoints _playerHitpoints;

        // Use this for initialization
        void Start ()
        {          
            _playerControl = new PlayerControl();
            PlayerBounceLogic = new PlayerBounceLogic();
            _playerHitpoints = new PlayerHitpoints();

            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerRigidbody2D.freezeRotation = true;

            _playerFoot = GameObject.Find("PlayerFoot").AddComponent(typeof(PlayerFoot)) as PlayerFoot;
            _playerHead = GameObject.Find("PlayerHead").AddComponent(typeof(PlayerHead)) as PlayerHead;       
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
            _playerFoot.Collider2D.sharedMaterial.bounciness = PlayerBounceLogic.GetBouncePower();
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
