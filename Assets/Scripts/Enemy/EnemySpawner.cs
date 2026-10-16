using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Camera cam;

    private float halfHeight;
    private float halfWidth;

    [SerializeField] private Enemy slimePrefab;
    [SerializeField] private Enemy batPrefab;
    [SerializeField] private Enemy mushroomPrefab;

    private Dictionary<Enemy, ObjectPool<Enemy>> pools = new Dictionary<Enemy, ObjectPool<Enemy>>();

    private ObjectPool<Enemy> batPool;
    private ObjectPool<Enemy> slimePool;
    private ObjectPool<Enemy> mushroomPool;


    private float elapsedTime;

    private float spawnTimer;

    private int maxEnemies = 300;


    [System.Serializable]
    public class SpawnEntry
    {
        public Enemy enemyPrefab;
        public int amount;
    }

    [System.Serializable]
    public class Stage
    {
        public float spawnInterval;
        public SpawnEntry[] enemies;   
    }

    [SerializeField] private Stage[] stages;

    private int currentStage;

    [SerializeField] private float stageDuration = 60f;


    [SerializeField] private LootManager lootManager;


    void Start()
    {
        batPool = CreatePool(batPrefab);
        slimePool = CreatePool(slimePrefab);
        mushroomPool = CreatePool(mushroomPrefab);


        pools.Add(batPrefab, batPool);
        pools.Add(slimePrefab, slimePool);
        pools.Add(mushroomPrefab, mushroomPool);

        elapsedTime = 0;
        spawnTimer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

       currentStage = Mathf.Min(Mathf.FloorToInt(elapsedTime / stageDuration), stages.Length - 1);


        


        if (spawnTimer >= stages[currentStage].spawnInterval)
        {
            if (GetAllActiveEnemies() < maxEnemies)
            {
                Spawn();
            }

            spawnTimer -= stages[currentStage].spawnInterval;
        }

        spawnTimer += Time.deltaTime;
        elapsedTime += Time.deltaTime;

    }

    private void Spawn()
    {
        float radius = Mathf.Sqrt(halfHeight * halfHeight + halfWidth * halfWidth);


        foreach (SpawnEntry entry in stages[currentStage].enemies)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                Vector2 spawnPosition = (Vector2)cam.transform.position + direction * radius;


                ObjectPool<Enemy> pool = pools[entry.enemyPrefab];


                if (GetAllActiveEnemies() >= maxEnemies) break;


                var enemy = pool.Get();
                enemy.transform.position = spawnPosition;


                enemy.OnDeath(e =>
                {
                    lootManager.DropLoot(e);
                    pool.Release(e);
                });

                enemy.OnDespawn(e =>
                {
                    pool.Release(e);
                });
            }
        }
        
    }

    private ObjectPool<Enemy> CreatePool(Enemy prefab)
    {
        return new ObjectPool<Enemy>(() =>
        {
            return Instantiate(prefab);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
            ;
        }, true, 10, maxEnemies);
    }

    private int GetAllActiveEnemies()
    {
        int totalActiveEnemies = 0;

        foreach (ObjectPool<Enemy> p in pools.Values)
        {
            totalActiveEnemies += p.CountActive;
        }

        return totalActiveEnemies;
    }
}
