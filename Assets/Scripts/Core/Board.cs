using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform emptySprite;
    public int height = 30;
    public int width = 10;
    public int header = 8;

    Transform[,] grid;

    public int completedRows = 0;

    private void Awake()
    {
        grid = new Transform[width, height];
    }

    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }

    bool IsOccupied(int x, int y, Shape shape)
    {
        return (grid[x, y] != null && grid[x, y].parent != shape.transform);
    }

    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);

            if (!IsWithinBoard((int) pos.x, (int) pos.y))
            {
                return false;
            }

            if (IsOccupied((int) pos.x, (int) pos.y, shape))
            {
                return false;
            }
        }

        return true;
    }

    private void DrawEmptyCells()
    {
        if (emptySprite != null)
        {
            for (int y = 0; y < height - header; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Transform clone;
                    clone = Instantiate(emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                    clone.name = "Board Space ( x = " + x.ToString() + ", y = " + y.ToString() + " )";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("WARNING! Please assing the emptySprite object!");
        }
    }

    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null)
        {
            return;
        }

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    public bool IsComplete(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
            grid[x, y] = null;
        }
    }

    void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    void ShiftRowsDown(int startY)
    {
        for (int i = startY; i < height; ++i)
        {
            ShiftOneRowDown(i);
        }
    }

    public void ClearAllRows()
    {
        completedRows = 0;

        for (int y = 0; y < height; ++y)
        {
            if (IsComplete(y))
            {
                ++completedRows;

                ClearRow(y);
                ShiftRowsDown(y);
                --y;
            }
        }
    }

    public bool IsOverLimit(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= (height - header - 1))
            {
                return true;
            }
        }

        return false;
    }
}
