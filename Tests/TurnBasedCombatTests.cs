using System;
using NUnit.Framework;
using RPGStats.Characters;
using RPGStats.Combat;

namespace Tests
{
    public class CombatTests
    {
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

        [Test]
        public void CanQueueCombatState()
        {
            ICombatState stateA = new TestCombatState();
            var combat = new TurnBasedCombat();
            
            Assert.DoesNotThrow(() =>
            { 
                combat.EnqueueState(stateA);  
            });
        }

        [Test]
        public void ThrowsExceptionPassingNullState()
        {
            var combat = new TurnBasedCombat();

            Assert.Throws<ArgumentNullException>(() =>
            {
                combat.EnqueueState(null);
            });
        }

        [Test]
        public void GoesToNextCombatStateWhenCurrentStateCompletes()
        {
            bool eventFired = false;
            TestCombatState stateA = new TestCombatState();
            stateA.OnComplete += () =>
            {
                eventFired = true;
            };
            
            ICombatState stateB = new TestCombatState();
            
            var combat = new TurnBasedCombat();
            combat.EnqueueState(stateA);
            combat.EnqueueState(stateB);
            combat.Begin();
            stateA.Complete();
            
            Assert.AreEqual(true, eventFired);
        }

        [Test]
        public void BeginCombatEventFires()
        {
            bool begin = false;
            
            var combat = new TurnBasedCombat();
            var state = new TestCombatState();
            combat.EnqueueState(state);
            combat.OnBegin += () =>
            {
                begin = true;
            };
            
            combat.Begin();
            
            Assert.AreEqual(true, begin);
        }
        
        [Test]
        public void EndCombatEventFires()
        {
            bool begin = false;
            
            var combat = new TurnBasedCombat();
            var state = new TestCombatState();
            combat.EnqueueState(state);
            combat.OnComplete += () =>
            {
                begin = true;
            };
            
            combat.Begin();
            state.Complete();
            
            Assert.AreEqual(true, begin);
        }

        [Test]
        public void ThrowsDuplicateStateAddedIfSameStateInstanceIsAddedMoreThanOnce()
        {
            var combat = new TurnBasedCombat();
            var state = new TestCombatState();
            combat.EnqueueState(state);

            Assert.Throws<DuplicateStateAddedException>(() =>
            {
                combat.EnqueueState(state);
            });
        }
        
        private class TestCombatState : ICombatState
        {
            public event Action OnComplete;

            public void Complete()
            {
                OnComplete?.Invoke();
            }
        }
    }
}