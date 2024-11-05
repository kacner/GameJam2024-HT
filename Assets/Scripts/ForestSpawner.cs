using UnityEngine;

public class ForestSpawner : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1f;
    public float rowOverlap = 0.2f;

    [Header("Sprites")]
    public Sprite[] ForestTiles;
    public GameObject Prefab;
    public GameObject lastRowPrefab;

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        if (ForestTiles == null || Prefab == null || lastRowPrefab == null)
        {
            Debug.LogError("Prefabs or ForestTiles are not assigned!");
            return;
        }

        Vector2 startPosition = (Vector2)transform.position - new Vector2((columns - 1) * cellSize / 2, (rows - 1) * cellSize / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate spawn position with row overlap
                Vector2 spawnPosition = startPosition + new Vector2(col * cellSize, row * cellSize - row * rowOverlap);

                int spawnType = Random.Range(0, ForestTiles.Length);

                // Use firstRowPrefab for the first row, otherwise use the default Prefab
                GameObject prefabToUse = (row == 0) ? lastRowPrefab : Prefab;

                GameObject square = Instantiate(prefabToUse, spawnPosition, Quaternion.identity, transform);
                square.transform.localScale = Vector3.one * cellSize;
                square.GetComponent<SpriteRenderer>().sprite = ForestTiles[spawnType];
            }
        }
    }
}