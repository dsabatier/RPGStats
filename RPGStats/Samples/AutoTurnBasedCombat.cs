using System;
using RPGStats.Characters;
using RPGStats.Combat;

namespace RPGStats.Samples
{
    public class AutoTurnBasedCombat : TurnBasedCombat
    {
        public AutoTurnBasedCombat()
        {
            Character knight = Character.Create<Knight>("Leroy");
            Character enemy = Character.Create<Wizard>("Evil Wizard");
            
            AddPlayer(knight);
            AddEnemy(enemy);

            var playersTurn = new AutoCombatState(this, GetPlayer(), GetEnemy());
            playersTurn.OnComplete += HandleCombatStateComplete;
            EnqueueState(playersTurn);
        }

        private void HandleCombatStateComplete(ICombatState state)
        {
            Character player = GetPlayer();
            Character enemy = GetEnemy();

            if (player.GetCurrentHealth() <= 0 || enemy.GetCurrentHealth() <= 0)
            {
                EndCombat();
            }
            else
            {
                if (state.GetOwner() == GetEnemy())
                {
                    EnqueueState(CreatePlayerTurn());
                }
                else
                {
                    EnqueueState(CreateEnemyTurn());
                }
            }
        }

        private AutoCombatState CreatePlayerTurn()
        {
            var newState = new AutoCombatState(this, GetPlayer(), GetEnemy());
            newState.OnComplete += HandleCombatStateComplete;

            return newState;
        }

        private AutoCombatState CreateEnemyTurn()
        {
            {
                var newState = new AutoCombatState(this, GetEnemy(), GetPlayer());
                newState.OnComplete += HandleCombatStateComplete;

                return newState;
            }
        }

        private void EndCombat()
        {
            Console.WriteLine("Combat Ended");
        }
    }
}