using System;
using RPGStats.Characters;

namespace RPGStats.Combat
{
    public class TurnBasedCombat
    {
        public event Action OnPlayerAdded;
        public event Action OnEnemyAdded;
        
        private Character _player;
        private Character _enemy;
        
        public void AddPlayer(Character player)
        {
            _player = player;
            OnPlayerAdded?.Invoke();
        }
        
        public void AddEnemy(Character enemy)
        {
            _enemy = enemy;
            OnEnemyAdded?.Invoke();
        }


        public Character GetPlayer()
        {
            return _player;
        }


        public Character GetEnemy()
        {
            return _enemy;
        }
    }
}