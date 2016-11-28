namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerHitpoints
    {
        private float hitpoints = 100;

        public float Hitpoints
        {
            get { return hitpoints; }
        }

        public void CalculateDamage()
        {
            hitpoints -= 50;
        }


    }
}
