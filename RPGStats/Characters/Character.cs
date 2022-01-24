using System;
using RPGStats.Stats;

namespace RPGStats.Characters
{
    public class Character
    {
        private readonly string _name = String.Empty;
        private int _currentLevel = 0;
        
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
            return newCharacter;
        }

        protected virtual void CreateStats()
        {
            _stats.AddStat(StatKeys.Health, 100, 10);
            _stats.AddStat(StatKeys.Defense, 100, 10);
            _stats.AddStat(StatKeys.Attack, 100, 10);
        }

        public double GetHealth()
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

        public object? GetName()
        {
            return _name;
        }
    }
}