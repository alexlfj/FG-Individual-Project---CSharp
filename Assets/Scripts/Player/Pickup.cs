using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Transform player;

    private xpItem xpItem;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Loot"))
        {
            xpItem = col.GetComponent<xpItem>();
            xpItem.isPickedUp = true;
        }
    }
}
