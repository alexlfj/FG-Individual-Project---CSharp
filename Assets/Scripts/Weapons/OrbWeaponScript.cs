using UnityEngine;

public class OrbWeaponScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private int damage;


    void Update()
    {
        transform.RotateAround(player.localPosition, Vector3.back, projectileSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("Enemy takes damage!");
        }
    }


}
