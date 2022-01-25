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
            var combat = new TurnBasedCombat();
        }

        [Test]
        public void CanAddPlayerToCombat()
        {
            var player = Character.Create<Character>(string.Empty);
            var combat = new TurnBasedCombat();
            
            Assert.DoesNotThrow(() =>
            {
                combat.AddPlayer(player);
            });
        }
        
        [Test]
        public void CanAddEnemyToCombat()
        {
            var enemy = Character.Create<Character>(string.Empty);
            var combat = new TurnBasedCombat();
            
            Assert.DoesNotThrow(() =>
            {
                combat.AddEnemy(enemy);
            });
        }

        [Test]
        public void CanSubscribeToPlayerAddedEvent()
        {
            var combat = new TurnBasedCombat();
            
            Assert.DoesNotThrow(() =>
            {
                combat.OnPlayerAdded += () =>
                {
                    // Empty
                };
            });
        }

        [Test]
        public void CanSubscribeToEnemyAddedEvent()
        {
            var combat = new TurnBasedCombat();
            
            Assert.DoesNotThrow(() =>
            {
                combat.OnEnemyAdded += () =>
                {
                    // Empty
                };
            });
            
        }

        [Test]
        public void CanGetPlayerFromCombat()
        {
            var player = Character.Create<Character>(string.Empty);
            var combat = new TurnBasedCombat();
            combat.AddPlayer(player);

            Assert.AreEqual(player, combat.GetPlayer());
        }
        
        [Test]
        public void CanGetEnemyFromCombat()
        {
            var enemy = Character.Create<Character>(string.Empty);
            var combat = new TurnBasedCombat();
            combat.AddEnemy(enemy);

            Assert.AreEqual(enemy, combat.GetEnemy());
        }

        [Test]
        public void PlayerAddedEventFires()
        {
            var player = Character.Create<Character>(string.Empty);
            var combat = new TurnBasedCombat();
            bool eventFired = false;

            combat.OnPlayerAdded += () =>
            {
                eventFired = true;
            };
            
            combat.AddPlayer(player);
            
            Assert.AreEqual(true, eventFired);
        }
        
        [Test]
        public void EnemyAddedEventFires()
        {
            var enemy = Character.Create<Character>(string.Empty);
            var combat = new TurnBasedCombat();
            bool eventFired = false;

            combat.OnEnemyAdded += () =>
            {
                eventFired = true;
            };
            
            combat.AddEnemy(enemy);
            
            Assert.AreEqual(true, eventFired);
        }
    }
}