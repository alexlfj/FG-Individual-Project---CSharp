using System.Collections;
using UnityEngine;

public class playerHealth : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    public float damageCooldown;

    public static playerHealth instance;

    private bool isInvincible = false;

    [SerializeField] private float InvincibilityDurationSeconds;
    [SerializeField] private float InvincibilityDeltaTime;

    private SpriteRenderer sprite;

    
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

        damageCooldown = InvincibilityDurationSeconds;

        sprite = GetComponentInParent<SpriteRenderer>();
    }


    public void Die()
    {
        Destroy(gameObject);
        print("You died!");
    }

    public void TakeDamage(int damageAmount)
    {

        if (isInvincible) return;

        
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(BecomeInvincible());
    }


    private IEnumerator BecomeInvincible()
    {
        Debug.Log("Player is invincible");
        isInvincible = true;

        for (float i = 0; i < InvincibilityDurationSeconds; i += InvincibilityDeltaTime)
        {
            if (sprite.enabled == true)
            {
                sprite.enabled = false;
            }
            else
            {
                sprite.enabled = true;
            }

            yield return new WaitForSeconds(InvincibilityDeltaTime);
        }

        Debug.Log("Player is not invincible");
        sprite.enabled = true;
        isInvincible = false;
    }

}

