using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGStats.Stats
{
    public class StatCollection
    {
        private readonly Dictionary<string, Stat> _stats = new();
        private readonly Dictionary<string, StatModification> _statModifications = new();

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
            return _statModifications.Values.Where(modification => modification.StatKey.Equals(statKey)).ToArray();
        }

        public StatModification AddModifier(string key, string statKey, double amount, StatModification.ModifierType type)
        {
            if (_statModifications.ContainsKey(key))
                throw new ModifierAlreadyPresentException();
            
            var statModification = new StatModification(key, statKey, amount, type);
            _statModifications[key] = statModification;
            return statModification;
        }

        public Stat AddStat(string statKey, double baseValue, double growthCoefficient)
        {
            if (_stats.ContainsKey(statKey))
                throw new StatAlreadyPresentException();
            
            _stats[statKey] = new BaseStat(baseValue, growthCoefficient);

            return _stats[statKey];
        }

        public void RemoveModifier(string key)
        {
            if (!_statModifications.ContainsKey(key))
                throw new ModifierNotFoundException();

            _statModifications.Remove(key);
        }
    }

    public class ModifierAlreadyPresentException : Exception
    {
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