using RPGStats.Combat;

namespace RPGStats.Samples
{
    public class DamageAbility : IDamage
    {
        private double _damageAmount;

        public DamageAbility(double damageAmount)
        {
            _damageAmount = damageAmount;
        }

        public double GetAmount()
        {
            return _damageAmount;
        }
    }
}