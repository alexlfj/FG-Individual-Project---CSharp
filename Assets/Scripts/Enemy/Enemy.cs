using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    public EnemySettings settings;

    public int CurrentHealth { get; set; }
    
    private float lastDamageTime;

    private playerHealth player;

    private Vector2 moveTarget;

    private Action<Enemy> onDeath;


    private float waveOffset;

    private Vector2 straightlineDirection;

    private Action<Enemy> onDespawn;

    private Camera cam;


    [SerializeField] private AudioClip[] enemyHit;


    [SerializeField] private Material flashMaterial;
    [SerializeField] private Material originalMaterial;
    private SpriteRenderer sprite;

    [SerializeField] private float flashDuration = 0.1f;



    void Start()
    {
        player = playerHealth.instance;
        cam = Camera.main;
        
    }

    void OnEnable()
    {
        CurrentHealth = settings.maxHealth;
        waveOffset = UnityEngine.Random.Range(0f, 2 * Mathf.PI);
        straightlineDirection = Vector2.zero;
        
        sprite = GetComponent<SpriteRenderer>();
        sprite.sharedMaterial = originalMaterial;
    }


    // Movement
    void FixedUpdate()
    {
        if (player == null) return; // Exits early if the player is dead (to prevent null reference errors)

        moveTarget = player.transform.position;

        Vector2 direction = (moveTarget - (Vector2)transform.position).normalized;

        switch (settings.behavior)
        {
            case EnemyBehaviorType.Chase:
                
                transform.position = Vector2.MoveTowards(transform.position, moveTarget, settings.speed * Time.fixedDeltaTime);

                break;
            
            
            case EnemyBehaviorType.Zigzag:
                
                Vector2 side = new Vector2(-direction.y, direction.x);

                float wave = Mathf.Sin(Time.time * settings.waveSpeed + waveOffset) * settings.waveWidth;

                Vector2 velocity = (direction + side * wave) * settings.speed;

                transform.position = (Vector2)transform.position + velocity * Time.fixedDeltaTime;

                break;
            
            
            case EnemyBehaviorType.Straightline:

                if (straightlineDirection == Vector2.zero)
                {
                    straightlineDirection = direction;
                }

                transform.position = (Vector2)transform.position + straightlineDirection * settings.speed * Time.fixedDeltaTime;

                float halfHeight = cam.orthographicSize;
                float halfWidth = halfHeight * cam.aspect;
                float radius = Mathf.Sqrt(halfHeight * halfHeight + halfWidth * halfWidth);

                if (Vector2.Distance(transform.position, moveTarget) > radius * 1.5f)
                {
                    Despawn();
                }

                break;

        }



    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(settings.attackDamage);
            lastDamageTime = Time.time;
        }
        
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        
        float damageTimeDiff = Time.time - lastDamageTime;

        if (col.gameObject.CompareTag("Player") && damageTimeDiff > player.damageCooldown)
        {
            player.TakeDamage(settings.attackDamage);
            lastDamageTime = Time.time;
        }
    }


    // Damage and dying 
    public void TakeDamage(int damageAmount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= damageAmount;

        PlayOnHit();

        sprite.sharedMaterial = flashMaterial;
        Invoke(nameof(ResetMaterial), flashDuration);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        onDeath(this);
    }

    public void OnDeath(Action<Enemy> deathAction)
    {
        onDeath = deathAction;
    }

    public void Despawn()
    {
        onDespawn(this);
    }
    public void OnDespawn(Action<Enemy> despawnAction)
    {
        onDespawn = despawnAction;
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
            SoundFXManager.Instance.PlayRandomAudioClips(enemyHit, transform, .4f);
        }
    }

    private void ResetMaterial()
    {
        sprite.sharedMaterial = originalMaterial;
    }
}
