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


    void Start()
    {
        player = playerHealth.instance;

    }

    void OnEnable()
    {
        CurrentHealth = settings.maxHealth;
    }


    // Movement
    void FixedUpdate()
    {
        if (player == null) return; // Exits early if the player is dead (to prevent null reference errors)

        //Movement
        moveTarget = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, settings.speed * Time.fixedDeltaTime);       

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
    public void Die()
    {
        onDeath(this);
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void OnDeath(Action<Enemy> deathAction)
    {
        onDeath = deathAction;
    }
}
