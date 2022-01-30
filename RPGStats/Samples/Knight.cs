using RPGStats.Characters;
using RPGStats.Combat;

namespace RPGStats.Samples
{
    public class Knight : Character
    {
        private double _currentHealth = 0;
        public Knight(string name) : base(name)
        {

        }

        protected override void CreateStats()
        {
            _stats.AddStat(Stats.StatKeys.Health, 150, 15);
            _stats.AddStat(Stats.StatKeys.Defense, 120, 10);
            _stats.AddStat(Stats.StatKeys.Attack, 130, 5);
            _stats.AddStat(Stats.StatKeys.MagicDefense, 60, 5);
            _stats.AddStat(Stats.StatKeys.MagicAttack, 50, 1);
        }
    }
}