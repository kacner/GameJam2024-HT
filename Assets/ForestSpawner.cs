using UnityEngine;

public class ForestSpawner : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1f;

    [Header("Prefabs")]
    public GameObject[] ForestTiles;
    

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        if (ForestTiles == null)
        {
            Debug.LogError("No square prefab assigned!");
            return;
        }

        // Calculate the starting position relative to the transform's position
        Vector2 startPosition = (Vector2)transform.position - new Vector2((columns - 1) * cellSize / 2, (rows - 1) * cellSize / 2);

        // Loop through rows and columns to create the grid
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate the spawn position relative to startPosition
                Vector2 spawnPosition = startPosition + new Vector2(col * cellSize, row * cellSize);

                // Instantiate the square at the calculated position
                int SpawnType = Random.Range(0, ForestTiles.Length);

                GameObject square = Instantiate(ForestTiles[SpawnType], spawnPosition, Quaternion.identity, transform);
                square.transform.localScale = Vector3.one * cellSize;
            }
        }
    }
}