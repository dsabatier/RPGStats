using System;
using RPGStats.Combat;
using RPGStats.Stats;

namespace RPGStats.Characters
{
    public class Character : IDamageable
    {
        private readonly string _name = String.Empty;
        private int _currentLevel = 0;
        private double _currentHealth;
        
        protected readonly StatCollection _stats = new();

        protected Character(string name)
        {
            _name = name;
            _currentLevel = 1;
        }

        public static Character Create<T>(string name) where T : Character
        {
            var newCharacter = new Character(name);
            newCharacter.CreateStats();
            newCharacter._currentHealth = newCharacter._stats.GetValue(StatKeys.Health, newCharacter._currentLevel);
            
            return newCharacter;
        }

        protected virtual void CreateStats()
        {
            _stats.AddStat(StatKeys.Health, 100, 10);
            _stats.AddStat(StatKeys.Defense, 100, 10);
            _stats.AddStat(StatKeys.Attack, 100, 10);
        }

        public double GetMaxHealth()
        {
            return _stats.GetValue(StatKeys.Health, _currentLevel);
        }

        public double GetDefense()
        {
            return _stats.GetValue(StatKeys.Defense, _currentLevel);
        }

        public double GetAttack()
        {
            return _stats.GetValue(StatKeys.Attack, _currentLevel);
        }

        public string GetName()
        {
            return _name;
        }

        public void TakeDamage(IDamage damage)
        {
            _currentHealth -= damage.GetAmount();
        }

        public double GetCurrentHealth()
        {
            return _currentHealth;
        }
    }
}