using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 21; // width of the maze
    public int height = 21; // height of the maze
    public GameObject floorPrefab; // prefab for the floor
    public GameObject wallPrefab; // prefab for the wall

    private int[,] maze;

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        maze = new int[width, height];

        // Initialize the maze with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // 1 represents a wall
            }
        }

        // Carve out paths using Recursive Backtracking
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int current = new Vector2Int(1, 1);
        maze[current.x, current.y] = 0; // 0 represents a floor
        stack.Push(current);

        while (stack.Count > 0)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var direction in directions)
            {
                Vector2Int neighbor = current + direction * 2;
                if (IsInBounds(neighbor) && maze[neighbor.x, neighbor.y] == 1)
                {
                    neighbors.Add(neighbor);
                }
            }

            if (neighbors.Count > 0)
            {
                Vector2Int next = neighbors[Random.Range(0, neighbors.Count)];
                maze[next.x, next.y] = 0;
                maze[current.x + (next.x - current.x) / 2, current.y + (next.y - current.y) / 2] = 0;
                stack.Push(next);
                current = next;
            }
            else
            {
                current = stack.Pop();
            }
        }
    }

    bool IsInBounds(Vector2Int position)
    {
        return position.x > 0 && position.x < width - 1 && position.y > 0 && position.y < height - 1;
    }

    void BuildMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, y, 0); // Change to (x, y, 0) for 2D plane
                if (maze[x, y] == 0)
                {
                    Instantiate(floorPrefab, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
