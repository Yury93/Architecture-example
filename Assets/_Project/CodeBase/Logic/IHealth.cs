using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        public Action HealthChanged { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public void TakeDamage(int damage);
    }
}
