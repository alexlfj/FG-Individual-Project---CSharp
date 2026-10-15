using UnityEngine;
using System.Collections.Generic;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private float orbitRadius;

    private playerStats playerStats;
    private playerHealth player;
    private List<GameObject> orbs = new List<GameObject>();

    void Start()
    {
        player = playerHealth.instance;
        playerStats = player.GetComponent<playerStats>();
    }

    void Update()
    {
        if (orbs.Count < playerStats.projectileCount)
        {
            Vector3 spawnPosition = player.transform.position + Vector3.right * orbitRadius;
            GameObject newOrb = Instantiate(orbPrefab, spawnPosition, Quaternion.identity, player.transform);
            orbs.Add(newOrb);

            ArrangeOrbs();
        }
    }

    void ArrangeOrbs()
{
    for (int i = 0; i < orbs.Count; i++)
    {
        float angle = 360f / orbs.Count * i;
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
        orbs[i].transform.position = player.transform.position + direction * orbitRadius;
    }
}
}
