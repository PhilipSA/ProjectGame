using UnityEngine;
using Random = System.Random;

namespace InteractingObjects.Player
{
    public class PlayerBounceLogic {

        public const float MaximumBouncePower = 5.0f;
        public const float MinimumBouncePower = 1.0f;
        public const float BouncePowerIncrease = 1.0f;
        public const float BouncePowerDecrease = 0.008f;
        public float BouncePower = 1.0f;
        // Use this for initialization

        public float GetBouncePower()
        {          
            return BouncePower;
        }

        public float GetRandomBouncePower()
        {
            //var random = new Random();
            //return random.Next((int) BouncePower, (int) (BouncePower + 0.1f));
            return BouncePower;
        }

        public void UpdateBouncePower()
        {
            BouncePower -= BouncePowerDecrease;
            BouncePower = Mathf.Clamp(BouncePower -= BouncePowerDecrease, MinimumBouncePower, MaximumBouncePower);
        }

        public void IncreaseBouncePower()
        {
            if (BouncePower < MaximumBouncePower) BouncePower += BouncePowerIncrease;
        }

        public void DecreaseBouncePower()
        {
            if (BouncePower > MinimumBouncePower) BouncePower -= BouncePowerIncrease;
        }
    }
}
