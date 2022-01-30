namespace RPGStats.Combat
{
    public interface IDamageable
    {
        void TakeDamage(IDamage damage);
        double GetCurrentHealth();
    }
}