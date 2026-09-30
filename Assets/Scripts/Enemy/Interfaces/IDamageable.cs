using UnityEngine;

public interface IDamageable 
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }

    void Die();

    void TakeDamage(int damageAmount);

}
