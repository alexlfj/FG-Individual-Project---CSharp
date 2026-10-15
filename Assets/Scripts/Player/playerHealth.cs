using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private ScoreManager scoreManager;

    private playerStats playerStats;

    private float recoveryProgress;

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

    void Start()
    {
        scoreManager = ScoreManager.instance;
        playerStats = GetComponent<playerStats>();
    }

    void Update()
    {
        recoveryProgress += playerStats.recovery * Time.deltaTime;

        if (recoveryProgress >= 1f)
        {
            int healthToRecover = Mathf.FloorToInt(recoveryProgress);
            CurrentHealth = Mathf.Min(CurrentHealth + healthToRecover, MaxHealth);
            recoveryProgress -= healthToRecover;
        }

    }

    public void Die()
    {
        Destroy(gameObject);

        SceneManager.LoadScene("MainMenu");

        scoreManager.SaveHighScore();
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
        isInvincible = true;

        for (float time = 0; time < InvincibilityDurationSeconds; time += InvincibilityDeltaTime)
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

        playerSprite.enabled = true;
        isInvincible = false;
    }

}

