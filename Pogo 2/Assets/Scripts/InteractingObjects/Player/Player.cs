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

        public PlayerControl PlayerControl;
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
            PlayerControl = new PlayerControl(this);
            PlayerBounceLogic = new PlayerBounceLogic();
            PlayerHitpoints = new PlayerHitpoints(this);

            BoxCollider2D = GetComponent<BoxCollider2D>();
        }
	
        // Update is called once per frame
        void Update()
        {
            PlayerBounceLogic.UpdateBouncePower();
            PlayerControl.StraightenUp();
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
            if (inputDeviceEnum == InputDeviceEnum.KeyboardAndMouse) PlayerControl.AnglePlayerTowardsInputOnChange(0.5f);
            if (inputDeviceEnum == InputDeviceEnum.TouchDevice) PlayerControl.AnglePlayerTowardsInputOnChange(1.5f);
        }

        public void DeadCheck()
        {
            if (PlayerHitpoints.Hitpoints <= 0 && enabled)
            {
                GameEngineHelper.GetCurrentGameEngine().Defeat();
                PlayerHead.AnimateDeath();
            }
        }
    }
}
