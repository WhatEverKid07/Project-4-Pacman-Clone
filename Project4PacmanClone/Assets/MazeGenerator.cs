using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10; // Number of cells horizontally
    public int height = 10; // Number of cells vertically
    public Tilemap wallTilemap; // Tilemap for walls
    public Tilemap pathTilemap; // Tilemap for paths

    private bool[,] visited;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        visited = new bool[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                visited[i, j] = false; // Initialize all cells as unvisited
            }
        }

        RecursiveBacktracking(0, 0); // Start maze generation from the top-left corner
    }

    void RecursiveBacktracking(int x, int y)
    {
        visited[x, y] = true; // Mark current cell as visited

        // Directions for movement (right, left, down, up)
        int[] directions = { 1, 2, 3, 4 };
        directions = ShuffleArray(directions); // Randomize the order of directions

        foreach (int dir in directions)
        {
            int nx = x, ny = y;
            switch (dir)
            {
                case 1: // Right
                    nx += 1;
                    break;
                case 2: // Left
                    nx -= 1;
                    break;
                case 3: // Down
                    ny += 1;
                    break;
                case 4: // Up
                    ny -= 1;
                    break;
            }

            if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visited[nx, ny])
            {
                // Connect current cell to next cell
                pathTilemap.SetTile(new Vector3Int(nx, ny, 0), null); // Ensure path is clear
                RecursiveBacktracking(nx, ny);
            }
        }

        // Place walls around perimeter
        if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
        {
            wallTilemap.SetTile(new Vector3Int(x, y, 0), wallTilemap.GetTile(new Vector3Int(0, 0, 0)));
        }
        else
        {
            wallTilemap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }

    // Fisher-Yates shuffle for array
    int[] ShuffleArray(int[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        return arr;
    }
}
