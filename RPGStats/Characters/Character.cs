using System;
using RPGStats.Stats;

namespace RPGStats.Characters
{
    public class Character
    {
        private const string MaxHealth = "health";
        private const string Defense = "defense";
        private const string Attack = "attack";

        private string _name = String.Empty;
        private int _currentLevel = 0;
        
        private readonly StatCollection _stats = new();

        public Character()
        {
            _name = "Unnamed";
            _currentLevel = 1;
            
            AddDefaultStats();
        }

        private void AddDefaultStats()
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
        
    }
}