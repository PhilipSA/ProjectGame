namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerBounceLogic {

        public const float MaximumBouncePower = 2.8f;
        public const float MinimumBouncePower = 1.4f;
        public const float BouncePowerIncrease = 0.4f;
        public const float BouncePowerDecrease = 0.002f;
        public float BouncePower = 1.4f;
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
