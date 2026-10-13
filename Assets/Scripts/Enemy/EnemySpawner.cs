using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Camera cam;

    private float halfHeight;
    private float halfWidth;

    [SerializeField] private Enemy slimePrefab;

    private ObjectPool<Enemy> slimePool;

    private float elapsedTime;

    private float spawnTimer;

    private int maxEnemies = 300;


    [System.Serializable]
    public class Stage
    {
        public float spawnInterval;
        public int spawnAmount;

    }

    [SerializeField] private Stage[] stages;

    private int currentStage;

    [SerializeField] private float stageDuration = 60f;


    [SerializeField] private LootManager lootManager;


    void Start()
    {

        elapsedTime = 0;
        spawnTimer = 0;

        // Slime pool
        slimePool = new ObjectPool<Enemy>(() =>
        {
            return Instantiate(slimePrefab);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        }, true, 10, 10);

    }

    // Update is called once per frame
    void Update()
    {
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

       currentStage = Mathf.Min(Mathf.FloorToInt(elapsedTime / stageDuration), stages.Length - 1);


        if (spawnTimer >= stages[currentStage].spawnInterval)
        {
            if (slimePool.CountActive < maxEnemies)
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


        for (int i = 0; i < stages[currentStage].spawnAmount; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = (Vector2)cam.transform.position + direction * radius;


            var enemy = slimePool.Get();
            enemy.transform.position = spawnPosition;

            if (slimePool.CountActive >= maxEnemies) break;

            enemy.OnDeath(e =>
            {
                lootManager.DropLoot(e);
                slimePool.Release(e);
            });
        }


        
    }
}
