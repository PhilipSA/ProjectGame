using System;

namespace Assets.Engine
{
    public class GameEvents
    {
        public delegate void GameEventTrigger();

        public event GameEventTrigger PlayerOnGoalCollision;

        public void OnPlayerGoalCollision()
        {
            if (PlayerOnGoalCollision != null) PlayerOnGoalCollision.Invoke();
        }
    }
}
