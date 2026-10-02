using UnityEngine;

public class Grid2D : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] int width = 8;
    [SerializeField] int height = 8;
    [SerializeField] float cellSize = 1f;

    GameObject[,] tiles;

    void Start()
    {
        tiles = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = Instantiate(tilePrefab, GridToWorld(x, y), Quaternion.identity, transform);
                tile.GetComponent<SpriteRenderer>().color = (x + y) % 2 == 0 ? Color.white : new Color(0.8f, 0.8f, 0.8f);
                tile.name = $"Tile {x},{y}";
                tiles[x, y] = tile;
            }
        }
    }

    public Vector3 GridToWorld(int x, int y)
    {
        float offsetX = (width - 1) * cellSize / 2f;
        float offsetY = (height - 1) * cellSize / 2f;
        return transform.position + new Vector3(x * cellSize - offsetX, y * cellSize - offsetY, 0f);

    }

    public Vector2Int WorldToGrid(Vector3 world)
    {
        Vector3 local = world - transform.position;
        return new Vector2Int(Mathf.RoundToInt(local.x / cellSize), Mathf.RoundToInt(local.y / cellSize));
    }

    public bool InBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;
}