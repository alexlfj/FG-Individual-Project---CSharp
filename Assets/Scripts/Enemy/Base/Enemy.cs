using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int MaxHealth { get; set; }

    public int CurrentHealth { get; set; }


    // Movement properties

    [SerializeField] private float speed;
    private Vector2 moveTarget;


    // Movement
    void FixedUpdate()
    {
        //Movement
        moveTarget = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            print("You are being attacked!");
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
