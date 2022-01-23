namespace RPGStats.Stats
{
    public class StatModification
    {
        public enum ModifierType
        {
            Additive,
            Percent
        }

        public readonly string StatKey;
        public readonly ModifierType Type; // additive or multiplicative
        public readonly double Amount; // 100, 30, 0.1, 0.12

        public StatModification(string statKey, double amount, ModifierType type)
        {
            Amount = amount;
            Type = type;
            StatKey = statKey;
        }
    }
}