using System;
using RPGStats.Characters;

namespace RPGStats.Combat
{
    public interface ICombatState
    {
        public event Action<ICombatState> OnComplete;
        public void Begin();
        public Character GetOwner();
    }
}