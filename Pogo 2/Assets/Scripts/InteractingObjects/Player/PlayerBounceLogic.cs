using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerBounceLogic {

        public const float MaximumBouncePower = 7.0f;
        public const float MinimumBouncePower = 2.0f;
        public const float BouncePowerIncrease = 1.0f;
        public const float BouncePowerDecrease = 0.006f;
        public float BouncePower = 2.0f;
        // Use this for initialization

        public float GetBouncePower()
        {          
            return BouncePower;
        }

        public float GetRandomBouncePower()
        {
            var random = new Random();
            return random.Next((int) BouncePower, (int) (BouncePower + BouncePowerIncrease));
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
