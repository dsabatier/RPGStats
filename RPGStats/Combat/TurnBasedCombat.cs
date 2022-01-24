using RPGStats.Characters;

namespace RPGStats.Combat
{
    public class TurnBasedCombat
    {
        private Character _player;
        private Character _enemy;

        private TurnBasedCombat()
        {

        }

        public static TurnBasedCombat Create()
        {
            return new TurnBasedCombat();
        }

        public void AddPlayer(Character player)
        {
            _player = player;
        }
        
        public void AddEnemy(Character enemy)
        {
            _enemy = enemy;
        }
    }
}