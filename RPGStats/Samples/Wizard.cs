using RPGStats.Characters;

namespace RPGStats.Samples
{
    public class Wizard : Character
    {
        protected Wizard(string name) : base(name)
        {
        }
        
        protected override void CreateStats()
        {
            _stats.AddStat(Stats.StatKeys.Health, 100, 5);
            _stats.AddStat(Stats.StatKeys.Defense, 80, 2.5);
            _stats.AddStat(Stats.StatKeys.Attack, 20, 5);
            _stats.AddStat(Stats.StatKeys.MagicDefense, 60, 55);
            _stats.AddStat(Stats.StatKeys.MagicAttack, 90, 35);
        }
        
    }
}