using UnityEngine;

public class OrbWeaponScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    private playerStats stats;

    void Start()
    {
        stats = player.GetComponent<playerStats>();
    }
    
    void Update()
    {
        transform.RotateAround(player.localPosition, Vector3.back, stats.attackSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Enemy target = col.GetComponent<Enemy>();

        if (target == null)
        {
            return;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            target.TakeDamage(stats.damage);
        }

    }


}
