using UnityEngine;
using Random = System.Random;

namespace InteractingObjects.Player
{
    public class PlayerBounceLogic {

        public const float MaximumBouncePower = 500.0f;
        public const float MinimumBouncePower = MaximumBouncePower / 5;
        public const float BouncePowerIncrease = MaximumBouncePower/5;
        public const float BouncePowerDecrease = MaximumBouncePower/2000;
        public float BouncePower = MinimumBouncePower;
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
