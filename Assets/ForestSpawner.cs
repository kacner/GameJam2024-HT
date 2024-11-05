using UnityEngine;

public class ForestSpawner : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1f;

    [Header("Sprites")]
    public Sprite[] ForestTiles;
    public GameObject Prefab;
    

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

        Vector2 startPosition = (Vector2)transform.position - new Vector2((columns - 1) * cellSize / 2, (rows - 1) * cellSize / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 spawnPosition = startPosition + new Vector2(col * cellSize, row * cellSize);

                int SpawnType = Random.Range(0, ForestTiles.Length);

                GameObject square = Instantiate(Prefab, spawnPosition, Quaternion.identity, transform);
                square.transform.localScale = Vector3.one * cellSize;
                square.GetComponent<SpriteRenderer>().sprite = ForestTiles[SpawnType];
            }
        }
    }
}