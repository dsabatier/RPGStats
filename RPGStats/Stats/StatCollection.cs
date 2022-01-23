using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGStats.Stats
{
    public class StatCollection
    {
        private readonly Dictionary<string, Stat> _stats = new();
        private readonly List<StatModification> _statModifications = new();

        public double GetBaseValue(string statKey, int level)
        {
            if (_stats.TryGetValue(statKey, out Stat stat))
            {
                return stat.GetBaseValue(level);
            }

            throw new StatNotFoundException();
        }

        public double GetValue(string statKey, int level)
        {
            double baseValue = GetBaseValue(statKey, level);

            var modifications = GetModifications(statKey);
            var (additiveValue, percentValue) = GetSortedModificationAmounts(modifications);

            return baseValue + additiveValue * (1 + percentValue);
        }

        private (double, double) GetSortedModificationAmounts(StatModification[] modifications)
        {
            double additiveAmount = 0;
            double percentAmount = 0;
            foreach (var statModification in modifications)
            {
                if (statModification.Type == StatModification.ModifierType.Additive)
                    additiveAmount += statModification.Amount;
                else
                    percentAmount += statModification.Amount;
            }

            return (additiveAmount, percentAmount);
        }

        private StatModification[] GetModifications(string statKey)
        {
            return _statModifications.Where(modification => modification.StatKey.Equals(statKey)).ToArray();
        }

        public StatModification AddModifier(string statKey, double amount, StatModification.ModifierType type)
        {
            var statModification = new StatModification(statKey, amount, type);
            _statModifications.Add(statModification);
            return statModification;
        }

        public Stat AddStat(string statKey, double baseValue, double growthCoefficient)
        {
            if (_stats.ContainsKey(statKey))
                throw new StatAlreadyPresentException();
            
            _stats[statKey] = new BaseStat(baseValue, growthCoefficient);

            return _stats[statKey];
        }

        public void RemoveModifier(StatModification modifier)
        {
            if (!_statModifications.Contains(modifier))
                throw new StatNotFoundException();

            _statModifications.Remove(modifier);
        }
    }

    public class StatAlreadyPresentException : Exception
    {
    }

    public class StatNotFoundException : Exception
    {
    }
    
    public class ModifierNotFoundException : Exception
    {
    }
}