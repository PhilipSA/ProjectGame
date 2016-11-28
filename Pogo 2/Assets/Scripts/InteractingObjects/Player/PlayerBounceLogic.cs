namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerBounceLogic {

        public const float MaximumBouncePower = 2.2f;
        public const float MinimumBouncePower = 1.4f;
        public const float BouncePowerIncrease = 0.2f;
        public const float BouncePowerDecrease = 0.002f;
        public float BouncePower = 1.4f;
        // Use this for initialization

        public float GetBouncePower()
        {          
            if (BouncePower > MinimumBouncePower) BouncePower -= BouncePowerDecrease;

            return BouncePower;
        }

        public void IncreaseBouncePower()
        {
            if (BouncePower < MaximumBouncePower) BouncePower += BouncePowerIncrease;
        }
    }
}
