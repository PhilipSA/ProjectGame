using Assets.Scripts.GUI;
using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D playerRigidbody2D;
        private PolygonCollider2D footCollider;
        private BoxCollider2D headCollider;

        private PlayerControl playerControl;
        private PlayerBounceLogic playerBounceLogic;
        private PlayerHitpoints playerHitpoints;

        // Use this for initialization
        void Start ()
        {
            playerControl = new PlayerControl();
            playerBounceLogic = new PlayerBounceLogic();
            playerHitpoints = new PlayerHitpoints();

            playerRigidbody2D = GetComponent<Rigidbody2D>();
            playerRigidbody2D.freezeRotation = true;
            var childComponents = GetComponentsInChildren<Component>();

            foreach (var component in childComponents)
            {
                if (component.name == "Foot")
                {
                    footCollider = component.GetComponent<PolygonCollider2D>();
                }
                if (component.name == "Head")
                {
                    headCollider = component.GetComponent<BoxCollider2D>();
                }
            }
        }
	
        // Update is called once per frame
        void Update ()
        {
            Bounce();
            if (playerControl.HasMousePositionChanged()) AnglePlayer();
            StraightenUp();
        }

        public void OnHeadCollision()
        {
            playerHitpoints.CalculateDamage();
            DeadCheck();
        }

        public void OnFootCollision()
        {
            AnglePlayer();
            MovePlayer();
        }

        void DeadCheck()
        {
            if (playerHitpoints.Hitpoints <= 0)
            {
                //LevelHandler.ChangeLevel("MainMenu");
            }
        }

        void Bounce()
        {
            var test = playerBounceLogic.GetBouncePower();
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, -test*10);
        }

        void MovePlayer()
        {
            var moveDirection = playerControl.GetMoveDirection(playerRigidbody2D);
            playerRigidbody2D.velocity = moveDirection;
        }

        void AnglePlayer()
        {
            var newRotationAngle = playerControl.GetRotationAngle(playerRigidbody2D);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotationAngle, 0.02f);
        }

        void StraightenUp()
        {
            var defaultAngle = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultAngle, 0.2f);
        }
    }
}
