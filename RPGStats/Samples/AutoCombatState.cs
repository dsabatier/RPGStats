using System;
using RPGStats.Characters;
using RPGStats.Combat;

namespace RPGStats.Samples
{
    public class AutoCombatState : ICombatState
    {
        private readonly AutoTurnBasedCombat _context;
        private readonly Character _character;
        private readonly Character _target;

        public AutoCombatState(AutoTurnBasedCombat context, Character character, Character target)
        {
            _context = context;
            _character = character;
            _target = target;

            OnComplete += state => { Console.WriteLine(state.ToString() + " state completed."); };
        }
        
        public event Action<ICombatState> OnComplete;
        public void Begin()
        {
            IDamage damage = new DamageAbility(20);
            Console.WriteLine($"{_character.GetName()} is attacking {_target.GetName()} for {damage.GetAmount()} damage!");
            _target.TakeDamage(damage);
            
            Console.WriteLine($"{_target.GetName()} has {_target.GetCurrentHealth()}/{_target.GetMaxHealth()} HP");
            OnComplete?.Invoke(this);
        }

        public Character GetOwner()
        {
            return _character;
        }
    }
}