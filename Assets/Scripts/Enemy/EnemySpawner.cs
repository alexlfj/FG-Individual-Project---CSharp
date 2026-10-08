using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;

    [SerializeField] private Camera cam;

    private float halfHeight;
    private float halfWidth;

    private float timer;

    [SerializeField] private float spawnInterval = 0.5f;

    void Start()
    {
        timer = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {

            Spawn();
            timer -= spawnInterval;
        }

        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

    }

    private void Spawn()
    {
        float radius = Mathf.Sqrt(halfHeight * halfHeight + halfWidth * halfWidth);
        Vector2 direction = Random.insideUnitCircle.normalized;

        Vector2 spawnPosition = (Vector2) cam.transform.position + direction * radius;

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
