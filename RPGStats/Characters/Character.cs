using System;
using RPGStats.Stats;

namespace RPGStats.Characters
{
    public class Character
    {
        private const string MaxHealth = "health";
        private const string Defense = "defense";
        private const string Attack = "attack";

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
            _stats.AddStat(MaxHealth, 100, 10);
            _stats.AddStat(Defense, 100, 10);
            _stats.AddStat(Attack, 100, 10);
        }

        public double GetHealth()
        {
            return _stats.GetValue(MaxHealth, _currentLevel);
        }

        public double GetDefense()
        {
            return _stats.GetValue(Defense, _currentLevel);
        }

        public double GetAttack()
        {
            return _stats.GetValue(Attack, _currentLevel);
        }

        public object? GetName()
        {
            return _name;
        }
    }
}