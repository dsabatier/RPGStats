using System;

namespace RPGStats.Combat
{
    public interface ICombatState
    {
        public event Action OnComplete;
    }
}