using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public Transform startPoint; // Reference to the start point

    private bool[,] maze;
    private List<Vector2Int> walls = new List<Vector2Int>();
    private List<Vector2Int> floors = new List<Vector2Int>();

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        maze = new bool[width, height];

        // Ensure start point is within bounds and aligned with the grid
        int startX = Mathf.Clamp(Mathf.RoundToInt(startPoint.position.x), 0, width - 1);
        int startY = Mathf.Clamp(Mathf.RoundToInt(startPoint.position.y), 0, height - 1);
        Vector2Int startPos = new Vector2Int(startX, startY);

        // Initialize maze generation with Prim's Algorithm
        maze[startPos.x, startPos.y] = true;
        AddWallsAround(startPos);

        while (walls.Count > 0)
        {
            // Pick a random wall
            int randomIndex = Random.Range(0, walls.Count);
            Vector2Int wall = walls[randomIndex];
            walls.RemoveAt(randomIndex);

            // Check if the wall can be turned into a floor
            List<Vector2Int> neighbors = GetFloorNeighbors(wall);

            if (neighbors.Count == 1)
            {
                maze[wall.x, wall.y] = true;
                floors.Add(wall);
                AddWallsAround(wall);
            }
        }

        DrawMaze();
    }

    void AddWallsAround(Vector2Int pos)
    {
        if (pos.x > 0 && !maze[pos.x - 1, pos.y]) walls.Add(new Vector2Int(pos.x - 1, pos.y));
        if (pos.x < width - 1 && !maze[pos.x + 1, pos.y]) walls.Add(new Vector2Int(pos.x + 1, pos.y));
        if (pos.y > 0 && !maze[pos.x, pos.y - 1]) walls.Add(new Vector2Int(pos.x, pos.y - 1));
        if (pos.y < height - 1 && !maze[pos.x, pos.y + 1]) walls.Add(new Vector2Int(pos.x, pos.y + 1));
    }

    List<Vector2Int> GetFloorNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (pos.x > 0 && maze[pos.x - 1, pos.y]) neighbors.Add(new Vector2Int(pos.x - 1, pos.y));
        if (pos.x < width - 1 && maze[pos.x + 1, pos.y]) neighbors.Add(new Vector2Int(pos.x + 1, pos.y));
        if (pos.y > 0 && maze[pos.x, pos.y - 1]) neighbors.Add(new Vector2Int(pos.x, pos.y - 1));
        if (pos.y < height - 1 && maze[pos.x, pos.y + 1]) neighbors.Add(new Vector2Int(pos.x, pos.y + 1));

        return neighbors;
    }

    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0); // Changed Z to 0 for 2D
                if (maze[x, y])
                {
                    Instantiate(floorPrefab, pos, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(wallPrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}
