using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable
{
    [field: SerializeField] public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }

    public Rigidbody2D RB { get; set; }

    public bool IsFacingRight { get; set; } = true;

    private void Start()
    {
        CurrentHealth = MaxHealth;

        RB = GetComponent<Rigidbody2D>();
    }

    // Health/Die functions
    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }



    // Movement functions
    public void MoveEnemy(Vector2 velocity)
    {
        RB.linearVelocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }

        if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }


    // Animation triggers
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {

    }

}
