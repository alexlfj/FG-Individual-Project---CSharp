using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;

    private Grid grid;

    void Start()
    {
        grid = GetComponent<Grid>();
        
    }

    void Update()
    {
        float xPosition = Mathf.Round(cameraTransform.position.x / grid.cellSize.x) * grid.cellSize.x;
        float yPosition = Mathf.Round(cameraTransform.position.y / grid.cellSize.y) * grid.cellSize.y;

        transform.position = new Vector3(xPosition, yPosition);

    }


}
