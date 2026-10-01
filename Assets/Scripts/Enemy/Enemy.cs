using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    
    [field: SerializeField] public int MaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    [SerializeField] private int attackDamage = 1;


    private playerHealth player;


    // Movement properties

    [SerializeField] private float speed;
    private Vector2 moveTarget;

    void Start()
    {
        player = playerHealth.instance;
    }

    // Movement
    void FixedUpdate()
    {
        if (player == null) return; // Exits early if the player is dead (to prevent null reference errors)

        //Movement
        moveTarget = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, speed * Time.fixedDeltaTime);  
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(attackDamage);
            print($"You are taking {attackDamage} damage!");
            print($"Your health is at {player.CurrentHealth}");
        }
        
    }



    // Damage and dying 
    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth < 0)
        {
            Die();
        }
    }
}
