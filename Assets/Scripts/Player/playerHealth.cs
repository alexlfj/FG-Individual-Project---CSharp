using UnityEngine;

public class playerHealth : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    [SerializeField] public float damageCooldown = 1f;

    public static playerHealth instance;

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        
        else
        {
            Destroy(gameObject);
        }


        CurrentHealth = MaxHealth;
    }


    public void Die()
    {
        Destroy(gameObject);
        print("You died!");
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }


}
