using UnityEngine;
using UnityEngine.Pool;
using System;

public class LootManager : MonoBehaviour
{

    private ObjectPool<xpItem> xpPool;

    [SerializeField] private xpItem xpItemPrefab;

    void Start()
    {
        xpPool = new ObjectPool<xpItem>(() =>
        {
            return Instantiate(xpItemPrefab);
        }, xpItem =>
        {
            xpItem.gameObject.SetActive(true);
        }, xpItem =>
        {
            xpItem.gameObject.SetActive(false);
        }, xpItem =>
        {
            Destroy(xpItem.gameObject);
        }, true, 10, 300);
    }


    public void DropLoot(Enemy enemy)
    {
        var xp = xpPool.Get();
        xp.SetExperience(enemy.xpValue);
        xp.transform.position = enemy.transform.position;

        xp.OnPickUp(e =>
        {
            xpPool.Release(e);
        });
    }
}
