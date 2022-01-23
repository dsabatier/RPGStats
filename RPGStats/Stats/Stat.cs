namespace RPGStats.Stats
{
    public abstract class Stat
    {
        protected double _baseValue;
        public abstract double GetBaseValue(int level);
    }
}