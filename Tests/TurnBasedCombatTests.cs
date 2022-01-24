using NUnit.Framework;
using RPGStats.Characters;
using RPGStats.Combat;

namespace Tests
{
    public class CombatTests
    {
        [Test]
        public void CanCreateCombat()
        {
            var combat = TurnBasedCombat.Create();
        }

        [Test]
        public void CanAddPlayerToCombat()
        {
            var player = Character.Create<Character>("Player");
            var combat = TurnBasedCombat.Create();
            
            Assert.DoesNotThrow(() =>
            {
                combat.AddPlayer(player);
            });
        }
        
        [Test]
        public void CanAddEnemyToCombat()
        {
            var enemy = Character.Create<Character>("Enemy");
            var combat = TurnBasedCombat.Create();
            
            Assert.DoesNotThrow(() =>
            {
                combat.AddEnemy(enemy);
            });
        }
    }
}