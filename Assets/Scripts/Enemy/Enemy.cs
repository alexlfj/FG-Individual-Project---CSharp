using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{

    [field: SerializeField] public int MaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    [SerializeField] private int attackDamage = 1;
    
    private float lastDamageTime;

    private playerHealth player;

    [SerializeField] private float speed;

    private Vector2 moveTarget;

    private Action<Enemy> onDeath;

    public float xpValue;

    public float scoreValue;



    void Start()
    {
        player = playerHealth.instance;

    }

    void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }


    // Movement
    void FixedUpdate()
    {
        if (player == null) return; // Exits early if the player is dead (to prevent null reference errors)

        //Movement
        moveTarget = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, speed * Time.fixedDeltaTime);       

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(attackDamage);
            print($"You are taking {attackDamage} damage!");
            print($"Your health is at {player.CurrentHealth}");
            lastDamageTime = Time.time;
        }
        
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        
        float damageTimeDiff = Time.time - lastDamageTime;

        if (col.gameObject.CompareTag("Player") && damageTimeDiff > player.damageCooldown)
        {
            player.TakeDamage(attackDamage);
            lastDamageTime = Time.time;

            print($"You are taking {attackDamage} damage!");
            print($"Your health is at {player.CurrentHealth}");
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
