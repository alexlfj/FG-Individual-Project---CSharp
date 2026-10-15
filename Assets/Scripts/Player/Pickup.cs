using UnityEngine;

public class Pickup : MonoBehaviour
{
    private xpItem xpItem;
    private playerStats playerStats;
    private CircleCollider2D pickupCollider;


    void Start()
    {
        playerStats = GetComponentInParent<playerStats>();
        pickupCollider = GetComponent<CircleCollider2D>();
    }
    
    void Update()
    {
        pickupCollider.radius = playerStats.pickupDistance;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Loot"))
        {
            xpItem = col.GetComponent<xpItem>();
            xpItem.isPickedUp = true;
        }
    }
}
