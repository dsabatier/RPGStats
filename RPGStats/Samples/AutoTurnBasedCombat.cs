using System;
using RPGStats.Combat;

namespace RPGStats.Samples
{
    public class AutoTurnBasedCombat : TurnBasedCombat
    {
        
    }

    public class AutoCombatState : ICombatState
    {
        public event Action? OnComplete;
        public void Begin()
        {
            
        }
    }
}