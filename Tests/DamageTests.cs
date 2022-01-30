using NUnit.Framework;
using RPGStats.Combat;

namespace Tests
{
    public class DamageTests
    {
        [Test]
        public void CanDamage()
        {
            IDamageable damageable = new TestDamageable();
            IDamage damage = new TestOneDamage();
            damageable.TakeDamage(damage);
            
            Assert.AreEqual(0, damageable.GetCurrentHealth());
        }
    }

    public class TestDamageable : IDamageable
    {
        public double DamageTaken { get; private set; } = 1;
        public void TakeDamage(IDamage damage)
        {
            DamageTaken -= damage.GetAmount();
        }

        public double GetCurrentHealth()
        {
            return DamageTaken;
        }
    }

    public class TestOneDamage : IDamage
    {
        public double GetAmount() => 1;
    }
}