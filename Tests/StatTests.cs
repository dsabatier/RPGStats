using System;
using NUnit.Framework;
using RPGStats.Stats;

namespace Tests
{
    public class StatTests
    {
        public const string Health = "health";
        public const string Defense = "defense";
        
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
        public void Stats_CanCreateStats()
        {
            Assert.DoesNotThrow(() =>
            {
                StatCollection statCollection = new StatCollection();
            });
        }

        [Test]
        public void Stats_CanGetBaseValueOfStat()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 0);
            
            Assert.AreEqual(100, statCollection.GetBaseValue(Health, 1));
        }

        [Test]
        public void Stats_ThrowsStatNotFoundException()
        {
            Assert.Throws<StatNotFoundException>(() =>
            {
                StatCollection statCollection = new StatCollection();
                Assert.AreEqual(100, statCollection.GetBaseValue("Fake", 1));
            });
        }

        [Test]
        public void Stats_CanAddModifier()
        {
            Assert.DoesNotThrow(() =>
            {
                StatCollection statCollection = new StatCollection();
                statCollection.AddModifier(Health, 100, StatModification.ModifierType.Additive);
            });
        }

        
        [Test]
        public void Stats_ThrowsExceptionAddingDuplicateStat()
        {
            StatCollection statCollection = new StatCollection();

            statCollection.AddStat(Health, 100, 10);

            Assert.Throws<StatAlreadyPresentException>(() =>
            {
                statCollection.AddStat(Health, 100, 10);
            });
        }
        
        [Test]
        public void Stats_CanGetModifiedValueOfStat()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            statCollection.AddModifier(Health, 100, StatModification.ModifierType.Additive);
            
            Assert.AreEqual(200, statCollection.GetValue(Health, 1));
        }

        [Test]
        public void Stats_ModificationsDontAffectBaseStat()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            statCollection.AddModifier(Health, 100, StatModification.ModifierType.Additive);
            
            Assert.AreEqual(100, statCollection.GetBaseValue(Health, 1));
        }

        [Test]
        public void Stats_AdditiveModsApplyBeforePercentMods()
        {
            StatCollection statCollection = new StatCollection();
            statCollection.AddStat(Health, 100, 5);
            statCollection.AddModifier(Health, 200, StatModification.ModifierType.Additive);
            statCollection.AddModifier(Health, 0.2, StatModification.ModifierType.Percent);
            
            Assert.AreEqual(100 + 200 * 1.2, statCollection.GetValue(Health, 1));
        }

        [Test]
        public void Stats_CanAddStatToStats()
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