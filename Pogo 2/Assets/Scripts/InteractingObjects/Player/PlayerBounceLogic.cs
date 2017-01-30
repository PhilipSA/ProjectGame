namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerBounceLogic {

        public const float MaximumBouncePower = 5.8f;
        public const float MinimumBouncePower = 1.0f;
        public const float BouncePowerIncrease = 0.4f;
        public const float BouncePowerDecrease = 0.008f;
        public float BouncePower = 1.0f;
        // Use this for initialization

        public float GetBouncePower()
        {          
            return BouncePower;
        }

        public void UpdateBouncePower()
        {
            if (BouncePower > MinimumBouncePower) BouncePower -= BouncePowerDecrease;
        }

        public void IncreaseBouncePower()
        {
            if (BouncePower < MaximumBouncePower) BouncePower += BouncePowerIncrease;
        }
    }
}
