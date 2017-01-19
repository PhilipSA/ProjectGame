namespace Assets.Scripts.Engine.Events
{
    public class GameEvents
    {
        public delegate void GameEventTrigger();

        public event GameEventTrigger PlayerOnGoalCollision;
        public event GameEventTrigger PlayerOnDefeat;

        public void OnPlayerGoalCollision()
        {
            if (PlayerOnGoalCollision != null) PlayerOnGoalCollision.Invoke();
        }

        public void OnPlayerDefeat()
        {
            if (PlayerOnDefeat != null) PlayerOnDefeat.Invoke();
        }
    }
}
