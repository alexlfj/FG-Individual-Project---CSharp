using UnityEngine;

public interface IDamageable
{
    void Damage(int damageAmount);

    void Die();

    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
}
