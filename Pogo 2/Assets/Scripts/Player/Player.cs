using Assets.Scripts.GUI;
using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _playerRigidbody2D;
        private PolygonCollider2D _footCollider;
        private BoxCollider2D _headCollider;

        private PlayerControl _playerControl;
        public PlayerBounceLogic PlayerBounceLogic;
        private PlayerHitpoints _playerHitpoints;

        private GUIHandler _guiHandler;

        // Use this for initialization
        void Start ()
        {          
            _guiHandler = gameObject.AddComponent<GUIHandler>();
            _playerControl = new PlayerControl();
            PlayerBounceLogic = new PlayerBounceLogic();
            _playerHitpoints = new PlayerHitpoints();

            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerRigidbody2D.freezeRotation = true;
            var childComponents = GetComponentsInChildren<Component>();

            foreach (var component in childComponents)
            {
                if (component.name == "Foot")
                {
                    _footCollider = component.GetComponent<PolygonCollider2D>();
                }
                if (component.name == "Head")
                {
                    _headCollider = component.GetComponent<BoxCollider2D>();
                }
            }
        }
	
        // Update is called once per frame
        void Update ()
        {
            Bounce();
            if (_playerControl.HasMousePositionChanged()) AnglePlayer();
            StraightenUp();
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

        public void OnGoalCollision()
        {
            Victory();
        }

        void Victory()
        {
            _guiHandler.DisplayVictoryScreen();
        }

        void DeadCheck()
        {
            if (_playerHitpoints.Hitpoints <= 0)
            {
                //LevelHandler.ChangeLevel("MainMenu");
            }
        }

        void Bounce()
        {
            _footCollider.sharedMaterial.bounciness = PlayerBounceLogic.GetBouncePower();
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
