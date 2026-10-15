using UnityEngine;

public class OrbWeaponScript : MonoBehaviour
{
    private playerHealth player;
    private playerStats stats;

    private Vector3 baseScale;

    void Start()
    {
        player = playerHealth.instance;
        stats = player.GetComponent<playerStats>();
        baseScale = transform.localScale;
    }
    
    void Update()
    {
        transform.RotateAround(player.transform.position, Vector3.back, stats.attackSpeed * Time.deltaTime);
        transform.localScale = baseScale * stats.areaOfEffect;
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
