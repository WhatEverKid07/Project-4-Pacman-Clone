using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator2D : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject pathPrefab;
    private int[,] maze;

    void Start()
    {
        GenerateMaze();
        CreateMazeInUnity();
    }

    void GenerateMaze()
    {
        maze = new int[width, height];
        List<Vector2Int> walls = new List<Vector2Int>();

        // Initialize the maze with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 0;
            }
        }

        // Start with a grid of cells (paths)
        for (int x = 1; x < width; x += 2)
        {
            for (int y = 1; y < height; y += 2)
            {
                maze[x, y] = 1;
                walls.Add(new Vector2Int(x, y));
            }
        }

        while (walls.Count > 0)
        {
            int randomIndex = Random.Range(0, walls.Count);
            Vector2Int cell = walls[randomIndex];
            walls.RemoveAt(randomIndex);

            List<Vector2Int> neighbors = GetValidNeighbors(cell);

            if (neighbors.Count > 0)
            {
                Vector2Int randomNeighbor = neighbors[Random.Range(0, neighbors.Count)];

                int x = (cell.x + randomNeighbor.x) / 2;
                int y = (cell.y + randomNeighbor.y) / 2;

                maze[x, y] = 1;
                walls.Add(randomNeighbor);
            }
        }
    }

    List<Vector2Int> GetValidNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (cell.x > 1 && maze[cell.x - 2, cell.y] == 1) neighbors.Add(new Vector2Int(cell.x - 2, cell.y));
        if (cell.x < width - 2 && maze[cell.x + 2, cell.y] == 1) neighbors.Add(new Vector2Int(cell.x + 2, cell.y));
        if (cell.y > 1 && maze[cell.x, cell.y - 2] == 1) neighbors.Add(new Vector2Int(cell.x, cell.y - 2));
        if (cell.y < height - 2 && maze[cell.x, cell.y + 2] == 1) neighbors.Add(new Vector2Int(cell.x, cell.y + 2));

        return neighbors;
    }

    void CreateMazeInUnity()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                if (maze[x, y] == 1)
                {
                    Instantiate(pathPrefab, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
