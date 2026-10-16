using System.Collections;
using UnityEngine;
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

    [SerializeField] private AudioClip[] playerHit;
    [SerializeField] private AudioClip[] playerDeath;

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
        PlayOnDeath();

        Destroy(gameObject);

        SceneManager.LoadScene("MainMenu");

        scoreManager.SaveHighScore();
    }

    public void TakeDamage(int damageAmount)
    {

        if (isInvincible) return;

        

        CurrentHealth -= damageAmount;
        PlayOnHit();

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

    private void PlayOnHit()
    {

        if (SoundFXManager.Instance == null)
        {
            Debug.LogWarning("SoundFXManager not found in scene.");
            return;
        }
        else
        {
            SoundFXManager.Instance.PlayRandomAudioClips(playerHit, transform, 1f);
        }
    }

    private void PlayOnDeath()
    {

        if (SoundFXManager.Instance == null)
        {
            Debug.LogWarning("SoundFXManager not found in scene.");
            return;
        }
        else
        {
            SoundFXManager.Instance.PlayRandomAudioClips(playerDeath, transform, .5f);
        }
    }


}

