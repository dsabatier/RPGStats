namespace RPGStats.Stats
{
    public class BaseStat : Stat
    {
        private readonly double _growthCoefficient;
        public BaseStat(double baseValue, double growthCoefficient)
        {
            _baseValue = baseValue;
            _growthCoefficient = growthCoefficient;
        }
    
        public override double GetBaseValue(int level)
        {
            return _baseValue + _growthCoefficient * (level - 1);
        }
    }
}