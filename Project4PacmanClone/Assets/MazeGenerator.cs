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
    private List<Vector2Int> stack = new List<Vector2Int>();

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        maze = new bool[width, height];
        Vector2Int startPos = new Vector2Int((int)startPoint.position.x, (int)startPoint.position.y);
        stack.Add(startPos);
        maze[startPos.x, startPos.y] = true;

        while (stack.Count > 0)
        {
            Vector2Int currentPos = stack[stack.Count - 1];
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(currentPos);

            if (neighbors.Count > 0)
            {
                Vector2Int nextPos = neighbors[Random.Range(0, neighbors.Count)];
                RemoveWall(currentPos, nextPos);
                stack.Add(nextPos);
                maze[nextPos.x, nextPos.y] = true;
            }
            else
            {
                stack.RemoveAt(stack.Count - 1);
            }
        }

        DrawMaze();
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (pos.x > 1 && !maze[pos.x - 2, pos.y]) neighbors.Add(new Vector2Int(pos.x - 2, pos.y));
        if (pos.x < width - 2 && !maze[pos.x + 2, pos.y]) neighbors.Add(new Vector2Int(pos.x + 2, pos.y));
        if (pos.y > 1 && !maze[pos.x, pos.y - 2]) neighbors.Add(new Vector2Int(pos.x, pos.y - 2));
        if (pos.y < height - 2 && !maze[pos.x, pos.y + 2]) neighbors.Add(new Vector2Int(pos.x, pos.y + 2));

        return neighbors;
    }

    void RemoveWall(Vector2Int current, Vector2Int next)
    {
        Vector2Int wallPos = current + (next - current) / 2;
        maze[wallPos.x, wallPos.y] = true;
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
