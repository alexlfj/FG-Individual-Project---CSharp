using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int MaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    public float damageCooldown;

    public static playerHealth instance;

    private bool isInvincible = false;

    [SerializeField] private float InvincibilityDurationSeconds, InvincibilityDeltaTime;

    private SpriteRenderer playerSprite;

    public Sprite fullHeart, halfHeart, emptyHeart;


    void Awake()
    {

        // Singleton instance check
        if (instance == null)
        {
            instance = this;
        } 
        
        else
        {
            Destroy(gameObject);
        }

        //Health
        CurrentHealth = MaxHealth;

        damageCooldown = InvincibilityDurationSeconds;

        playerSprite = GetComponentInParent<SpriteRenderer>();

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
            if (playerSprite.enabled == true)
            {
                playerSprite.enabled = false;
            }
            else
            {
                playerSprite.enabled = true;
            }

            yield return new WaitForSeconds(InvincibilityDeltaTime);
        }

        Debug.Log("Player is not invincible");
        playerSprite.enabled = true;
        isInvincible = false;
    }

}

