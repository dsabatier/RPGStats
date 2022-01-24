using System;
using NUnit.Framework;
using RPGStats.Stats;

namespace Tests
{
    public class StatTests
    {
        public const string Health = "health";
        public const string Defense = "defense";
        public const string AddModification = "add";
        public const string PercentModification = "pct";
        
        [Test]
        public void BaseStatValueIsSetByArg()
        {
            double value = 1;
            Stat stat = new BaseStat(value, 1);
            Assert.AreEqual(value, stat.GetBaseValue(1));
        }

        [Test]
        public void BaseStatValue_CanGetByLevel_CoefficientModifiesValueCorrectly()
        {
            int baseValue = 5;
            int growthCoefficient = 5;

            Stat stat = new BaseStat(baseValue, growthCoefficient);

            for (int i = 1; i < 20; i++)
            {
                Assert.AreEqual(baseValue * i, stat.GetBaseValue(i));
            }
        }

        [Test]
        public void BaseStatValue_CoefficientCanBeZero()
        {
            int baseValue = 5;
            int growthCoefficient = 0;

            Stat stat = new BaseStat(baseValue, growthCoefficient);

            for (int i = 1; i < 20; i++)
            {
                Assert.AreEqual(baseValue, stat.GetBaseValue(i));
            }
        }

        [Test]
        public void CanCreateStats()
        {
            Assert.DoesNotThrow(() =>
            {
                StatCollection statCollection = new StatCollection();
            });
        }

        [Test]
        public void CanGetBaseValueOfStat()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 0);
            
            Assert.AreEqual(100, statCollection.GetBaseValue(Health, 1));
        }

        [Test]
        public void ThrowsStatNotFoundException()
        {
            Assert.Throws<StatNotFoundException>(() =>
            {
                StatCollection statCollection = new StatCollection();
                Assert.AreEqual(100, statCollection.GetBaseValue("Fake", 1));
            });
        }

        [Test]
        public void CanAddModifier()
        {
            Assert.DoesNotThrow(() =>
            {
                StatCollection statCollection = new StatCollection();
                statCollection.AddModifier(AddModification, Health, 100, StatModification.ModifierType.Additive);
            });
        }

        
        [Test]
        public void ThrowsExceptionAddingDuplicateStat()
        {
            StatCollection statCollection = new StatCollection();

            statCollection.AddStat(Health, 100, 10);

            Assert.Throws<StatAlreadyPresentException>(() =>
            {
                statCollection.AddStat(Health, 100, 10);
            });
        }
        
        [Test]
        public void CanGetModifiedValueOfStat()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            statCollection.AddModifier(AddModification, Health, 100, StatModification.ModifierType.Additive);
            
            Assert.AreEqual(200, statCollection.GetValue(Health, 1));
        }

        [Test]
        public void CanRemoveModifier()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            var modifier = statCollection.AddModifier(AddModification, Health, 100, StatModification.ModifierType.Additive);
            statCollection.RemoveModifier(AddModification);
            
            Assert.AreEqual(100, statCollection.GetValue(Health, 1));
        }
        
        [Test]
        public void RemoveModifierThrowsModifierNotFound()
        {
            StatCollection statCollection = new StatCollection();

            Assert.Throws<ModifierNotFoundException>(() =>
            {
                statCollection.RemoveModifier(AddModification);
            });
        }
        
        [Test]
        public void AddModifierThrowsModifierAlreadyPresent()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 1, 0);
            statCollection.AddModifier(AddModification, Health, 10, StatModification.ModifierType.Additive);
            
            Assert.Throws<ModifierAlreadyPresentException>(() =>
            {
                statCollection.AddModifier(AddModification, Health, 10, StatModification.ModifierType.Additive);
            });
        }

        [Test]
        public void ModificationsDontAffectBaseStat()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            statCollection.AddModifier(AddModification, Health, 100, StatModification.ModifierType.Additive);
            
            Assert.AreEqual(100, statCollection.GetBaseValue(Health, 1));
        }

        [Test]
        public void AdditiveModsApplyBeforePercentMods()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            statCollection.AddModifier(AddModification, Health, 200, StatModification.ModifierType.Additive);
            statCollection.AddModifier(PercentModification,Health, 0.2, StatModification.ModifierType.Percent);
            
            Assert.AreEqual(100 + 200 * 1.2, statCollection.GetValue(Health, 1));
        }

        [Test]
        public void CanAddStatToStats()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 10);
            
            Assert.DoesNotThrow(() =>
            {
                statCollection.GetBaseValue(Health, 1);
            });
        }
    }
}